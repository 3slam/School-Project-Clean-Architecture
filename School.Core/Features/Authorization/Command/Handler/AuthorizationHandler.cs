﻿using AutoMapper;
using MediatR;

using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Authorization.Command.Model;
using School.Core.Features.Authorization.Command.Validations;
using School.Data.Constants;
using School.Data.Resourses;
using School.Service.IService;


namespace School.Core.Features.Authorization.Command.Handler
{
    public class AuthorizationHandler(IStringLocalizer<SharedResourses> localizer,
        IAuthorizationService authorization,
        IMapper mapper)
        : ResponseHandler(localizer),
        IRequestHandler<AddRoleCommand, Response<string>>,
        IRequestHandler<DeleteRoleCommand, Response<string>>,
        IRequestHandler<EditRoleCommand, Response<string>>,
        IRequestHandler<UpdateUserRolesCommand, Response<string>>

    {
        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var validator = new AddRoleValidation(authorization);
            var result = await validator.ValidateAsync(request);
            string error = "";
            if (result.IsValid == false)
            {
                foreach (var item in result.Errors)
                    error = error + item.ErrorMessage;
                return BadRequest<string>(error);
            }

            try
            {
                var addResult = await authorization.AddRoleAsync(request.RoleName);
                if (addResult == State.Success)
                    return Success("role added successfully");

                return BadRequest<string>("role added failed");
            }
            catch (Exception ex)
            {
                return BadRequest<string>(ex.Message);
            }
        }

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteRoleValidation(authorization);
            var result = await validator.ValidateAsync(request);
            string error = "";
            if (result.IsValid == false)
            {
                foreach (var item in result.Errors)
                    error = error + item.ErrorMessage;
                return BadRequest<string>(error);
            }

            try
            {
                var addResult = await authorization.DeleteRoleAsync(request.Id);
                if (addResult == State.Success)
                    return Success("UserRole Deleted successfully");

                return BadRequest<string>("UserRole Deleted failed");
            }
            catch (Exception ex)
            {
                return BadRequest<string>(ex.Message);
            }
        }

        public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var validator = new EditRoleValidation(authorization);
            var result = await validator.ValidateAsync(request);
            string error = "";
            if (result.IsValid == false)
            {
                foreach (var item in result.Errors)
                    error = error + item.ErrorMessage;
                return BadRequest<string>(error);
            }

            try
            {
                var addResult = await authorization.EditRoleAsync(request);
                if (addResult == State.Success)
                    return Success("UserRole Edited successfully");

                return BadRequest<string>("UserRole Edited failed");
            }
            catch (Exception ex)
            {
                return BadRequest<string>(ex.Message);
            }
        }

        public async Task<Response<string>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateUserRolesValidation(authorization);
            var result = await validator.ValidateAsync(request);
            string error = "";
            if (result.IsValid == false)
            {
                foreach (var item in result.Errors)
                    error = error + item.ErrorMessage;
                return BadRequest<string>(error);
            }

            var updatingResult = await authorization.UpdateUserRoles(request);

            if (updatingResult == State.Failed) return BadRequest<string>("Some Thing went incorrect");

            return Success("Roles Updated Correctly");
        }
    }
}
