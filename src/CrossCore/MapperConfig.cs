using AutoMapper;
using CrossCore.Dals;
using CrossCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCore
{
    public class MapperConfig
    {
        public static MapperConfiguration Config { get; set; }

        public static void Init()
        {
            Config = new AutoMapper.MapperConfiguration(p => p.CreateMap<Role, RoleDto>());
        }
    }
}
