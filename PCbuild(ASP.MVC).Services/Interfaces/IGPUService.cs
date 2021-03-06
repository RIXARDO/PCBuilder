﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCbuilder_ASP.MVC_.Services.DTO;

namespace PCbuilder_ASP.MVC_.Services.Interfaces
{
    public interface IGPUService
    {
        IEnumerable<GPUdto> GetGPUs();
        GPUdto GetGPUByID(Guid guid);
        void SaveGPU(GPUdto gpu);
        void EditGPU(GPUdto gpu);
        void DeleteGPU(Guid guid);
    }
}
