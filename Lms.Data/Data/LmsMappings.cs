﻿using AutoMapper;
using Lms.Api.Dto;
using Lms.Common.Dto;
using Lms.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Data
{
    public class LmsMappings : Profile
    {
        public LmsMappings()
        {
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Module, ModuleDto>().ReverseMap();
            CreateMap<Module, ModuleInsertDto>().ReverseMap();            
        }
    }
}
