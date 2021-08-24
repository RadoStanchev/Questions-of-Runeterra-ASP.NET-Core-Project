using AutoMapper;
using Microsoft.AspNetCore.Http;
using QuestionsOfRuneterra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionsOfRuneterra.Services.Uploads
{
    public interface IUploadService
    {
        string Upload(IFormFile file);

        bool Validate(IFormFile file);
    }
}
