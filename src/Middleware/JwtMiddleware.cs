﻿//-----------------------------------------------------------------------
// <copyright file="JwtMiddleware.cs" company="QJJS">
//     Copyright QJJS. All rights reserved.
// </copyright>
// <author>liyuhang</author>
// <date>2021/9/1 15:32:25</date>
//-----------------------------------------------------------------------

using FastDotnet.Entity.User;
using FastDotnet.Repository;
using FastDotnet.Schema.App;
using FastDotnet.Schema.User;
using FastDotnet.Utility;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace FastDotnet.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                await AttachUserToContext(context, token);
            }

            await _next(context);
        }

        private async Task AttachUserToContext(HttpContext context, string token)
        {
            if (JwtUtil.VerifyToken(token))
            {
                var userId = JwtUtil.SerializeJwt(token);
                var user = await new Repository<UserEntity>().Get<UserModel>(x => x.Id == userId);
                context.Items[AppConstant.UserContext] = user;
            }
        }
    }
}