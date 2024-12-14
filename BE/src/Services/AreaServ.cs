using BE.src.Domains.DTOs.Area;
using BE.src.Domains.Models;
using BE.src.Repositories;
using BE.src.Shared.Type;
using BE.src.Util;
using Microsoft.AspNetCore.Mvc;

namespace BE.src.Services
{
    public interface IAreaServ
    {
        Task<IActionResult> CreateArea(CreateAreaRqDTO data);
        Task<IActionResult> GetAreas();
        Task<IActionResult> UpdateArea(Guid id, UpdateAreaDTO data);
    }

    public class AreaServ : IAreaServ
    {
        private readonly IAreaRepo _areaRepo;

        public AreaServ(IAreaRepo areaRepo)
        {
            _areaRepo = areaRepo;
        }

        public async Task<IActionResult> CreateArea(CreateAreaRqDTO data)
        {
            try
            {
                var location = new Location
                {
                    Address = data.Address,
                    Longitude = data.Longitude,
                    Latitude = data.Latitude,
                };
                var isLocationCreated = await _areaRepo.CreateLocation(location);
                if (!isLocationCreated)
                {
                    return ErrorResp.BadRequest("Fail to create location");
                }
                else
                {
                    var area = new Area
                    {
                        Name = data.Name,
                        Description = data.Description,
                        Location = location,
                    };
                    var isAreaCreated = await _areaRepo.CreateArea(area);
                    if (!isAreaCreated)
                    {
                        return ErrorResp.BadRequest("Fail to create area");
                    }
                    else
                    {
                        foreach (IFormFile image in data.Images)
                        {
                            string? urlFirebase = await Utils.UploadImgToFirebase(image, Utils.ConvertToUnderscore(data.Name), "Area");
                            if (urlFirebase == null)
                            {
                                return ErrorResp.BadRequest("Fail to save image to firebase");
                            }
                            var imageObj = new Image
                            {
                                Area = area,
                                Url = urlFirebase
                            };
                            var isImageCreated = await _areaRepo.AddImageArea(imageObj);
                            if (!isImageCreated)
                            {
                                return ErrorResp.BadRequest("Fail to save image to database");
                            }
                        }
                        return SuccessResp.Created("Create area success");
                    }
                }
            }
            catch (Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> GetAreas()
        {
            try
            {
                var areas = await _areaRepo.GetAreas();
                return SuccessResp.Ok(areas);
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> UpdateArea(Guid id, UpdateAreaDTO data)
        {
            try
            {
                var area = await _areaRepo.GetAreaById(id);
                if (area == null)
                {
                    return ErrorResp.NotFound("Area not found");
                }    

                area.Name = data.Name ?? area.Name;
                area.Description = data.Description ?? area.Description;
                area.UpdateAt = DateTime.Now;

                var location = await _areaRepo.GetLocationById(area.LocationId);
                if (location == null)
                {
                    return ErrorResp.NotFound("Location not found");
                }

                if (area.Location == null)
                {
                    location.Address = location.Address;
                    location.Latitude = location.Latitude;
                    location.Longitude = location.Longitude;

                    var isLocationUpdated = await _areaRepo.UpdateLocation(location);
                    if (!isLocationUpdated)
                    {
                        return ErrorResp.BadRequest("Failed to update location");
                    }
                }
                else
                {
                    location.Address = data.Address ?? location.Address;
                    location.Latitude = data.Latitude ?? location.Latitude;
                    location.Longitude = data.Longitude ?? location.Longitude;
                    location.UpdateAt = DateTime.Now;

                    var isLocationUpdated = await _areaRepo.UpdateLocation(location);
                    if (!isLocationUpdated)
                    {
                        return ErrorResp.BadRequest("Failed to update location");
                    }
                    
                }

                var existingImages = await _areaRepo.GetImagesByAreaId(id);
                if (existingImages == null || existingImages.Count == 0)
                {
                    return ErrorResp.NotFound("Images not found");
                }

                if (data.Images == null || data.Images.Count == 0)
                {
                    area.Images = existingImages;

                    var updatedImages = await _areaRepo.UpdateSecondImageArea(existingImages);
                    if (!updatedImages)
                    {
                        return ErrorResp.BadRequest("Failed to update images");
                    }
                }
                else
                {
                    foreach (IFormFile imageFile in data.Images)
                    {
                        string? urlFirebase = await Utils.UploadImgToFirebase(imageFile, Utils.ConvertToUnderscore(area.Name), "Area");
                        if (urlFirebase == null)
                        {
                            return ErrorResp.BadRequest("Failed to save image to Firebase");
                        }

                        var imageArea = await _areaRepo.GetImageByAreaId(id);
                        if (imageArea == null)
                        {
                            return ErrorResp.NotFound("Image not found");
                        }

                        var newImage = new Image
                        {
                            Id = imageArea.Id,
                            Url = urlFirebase,
                            UpdateAt = DateTime.Now,
                        };

                        var isImageCreated = await _areaRepo.UpdateImageArea(newImage);
                        if (!isImageCreated)
                        {
                            return ErrorResp.BadRequest("Failed to update image");
                        }
                    }
                }

                var isAreaUpdated = await _areaRepo.UpdateArea(area);
                if (!isAreaUpdated)
                {
                    return ErrorResp.BadRequest("Failed to update area");
                }

                return SuccessResp.Ok("Area updated successfully");
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }
    }
}