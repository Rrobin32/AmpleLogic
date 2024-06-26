﻿using BussinessLayer.UserBL;
using ConfigurationUtilities.Generic;
using DataTransferObject.DBModel;
using Models.InputModel.UsersInputObj;
using Models.ResponseModel.UsersResponse;
using System.Text;

namespace AppServices.UserService
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserBLL _iuserBLL;
        private readonly IGenericAppServices _igenericAppServices;

        public UserAppService(IUserBLL userBLL, IGenericAppServices genericAppServices)
        {
            _iuserBLL = userBLL;    
            _igenericAppServices = genericAppServices;
        }
        public List<ResponseMessage> GetUserInfo(GetUserInfo getUserInfo, ref List<UserResponse> userResponse)
        {
            GetUserInfo userInfoDto = _igenericAppServices.AutoMapper<GetUserInfo, GetUserInfo>(getUserInfo);
            userResponse = _iuserBLL.GetUserInfo(userInfoDto);
            return _igenericAppServices.ConvertList(userResponse);
        }

        public List<ResponseMessage> AddUserInfo(AddUserInfo addUserInfo, ref AddUserInfoResponseObj userResponse)
        {
            AddUserInfo userInfoDto = _igenericAppServices.AutoMapper<AddUserInfo, AddUserInfo>(addUserInfo);
            StringBuilder errors = _iuserBLL.ValidateUser(userInfoDto);
            if(errors.Length > 0)
            {
                return _igenericAppServices.ConvertStringBuilder(errors);
            }
            else
            {
                _iuserBLL.AddUserInfo(userInfoDto);
                userResponse.Message = userInfoDto.Message;
                userResponse.Id = Convert.ToInt64(userInfoDto.Id);
                return _igenericAppServices.ConvertClass(userResponse);
            }            
        }

        public List<ResponseMessage> DeleteUserInfo(DeleteUserInfo deleteUserInfo, ref DeleteUserInfoResponseObj deleteUserInfoResponseObj)
        {
            DeleteUserInfo dto = _igenericAppServices.AutoMapper<DeleteUserInfo, DeleteUserInfo>(deleteUserInfo);
            _iuserBLL.DeleteUserInfo(dto);
            deleteUserInfoResponseObj.Message = dto.Message;
            return _igenericAppServices.ConvertClass(deleteUserInfoResponseObj);
        }

        public List<ResponseMessage> UpdateUserInfo(UpdateUserInfo updateUserInfo, ref UpdateUserInfoResponseObj updateUserInfoResponseObj)
        {
            UpdateUserInfo dto = _igenericAppServices.AutoMapper<UpdateUserInfo, UpdateUserInfo>(updateUserInfo);
            _iuserBLL.UpdateUserInfo(dto);
            updateUserInfoResponseObj.Message = dto.Message;
            return _igenericAppServices.ConvertClass(updateUserInfoResponseObj);
        }
    }
}
