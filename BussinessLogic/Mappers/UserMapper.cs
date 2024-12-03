using BussinessLogic.ViewModels;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Mappers
{
    public static class UserMapper
    {
        public static List<UserVM> ToViewList(this ICollection<User> listEntities )
        {
            return listEntities.Select(x => x.ToView()).ToList();
        }
        public static UserVM ToView(this User userEntity)
        {
            return new UserVM
            {
                Id = userEntity.Id,
                Name = userEntity.Name,
                Email = userEntity.Email,
                Password = userEntity.Password,
                Photo = userEntity.Photo,
            };
        }
        public static LightUserVM ToViewLight(this User userEntity)
        {
            return new LightUserVM
            {
                Id = userEntity.Id,
                Name = userEntity.Name,
                Photo = userEntity.Photo
            };
        }
        public static UserSessionVM ToViewSession(this User userEntity)
        {
            return new UserSessionVM
            {
                Id = userEntity.Id,
                Name = userEntity.Name,
                Email = userEntity.Email,
                Photo = userEntity.Photo,
                Works = userEntity.Works != null ? userEntity.Works.ToViewList() : new List<WorkVM>(),
                Token = userEntity.Token,
            };
        }

        public static User ToEntity(this UserVM user)
        {
            return new User
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Photo = user.Photo,
            };
        } 
        public static User ToRegisterEntity(this UserRegisterVM user)
        {
            return new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
            };
        }
    }
}
