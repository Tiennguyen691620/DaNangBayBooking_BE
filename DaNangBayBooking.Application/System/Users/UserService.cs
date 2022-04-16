using DaNangBayBooking.Data.EF;
using DaNangBayBooking.Data.Entities;
using DaNangBayBooking.ViewModels.Common;
using DaNangBayBooking.ViewModels.System.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using System.Linq;
using DaNangBayBooking.ViewModels.System.Roles;
using DaNangBayBooking.ViewModels.Catalog.BookRooms;
using DaNangBayBooking.ViewModels.Catalog.Locations;

namespace DaNangBayBooking.Application.System.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly DaNangDbContext _context;
        public UserService(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager,
            IConfiguration config,
            DaNangDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _context = context;
        }

        public async Task<ApiResult<LoginUser>> LoginAdmin(LoginRequest request)
        {
            
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) return new ApiErrorResult<LoginUser>("Tài khoản không tồn tại");

            var roles = await _roleManager.FindByIdAsync(user.AppRoleID.ToString());
            if (roles.Name.ToUpper() == "CLIENT") return new ApiErrorResult<LoginUser>("Chỉ nhận tài khoản quản trị viên.");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, true, true);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<LoginUser>("Đăng nhập không đúng");
            }
            //var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FullName),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            var loginUser = new LoginUser()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                Dob = user.Dob,
                Id = user.Id,
                UserName = user.UserName,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RoleName = roles.Name
            };
            return new ApiSuccessResult<LoginUser>(loginUser);
        }

        public async Task<ApiResult<LoginUser>> LoginClient(LoginRequest request)
        {

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) return new ApiErrorResult<LoginUser>("Tài khoản không tồn tại");

            var roles = await _roleManager.FindByIdAsync(user.AppRoleID.ToString());
            if (roles.Name.ToUpper() == "ADMIN") return new ApiErrorResult<LoginUser>("Chỉ nhận tài khoản khách hàng.");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, true, true);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<LoginUser>("Đăng nhập không đúng");
            }
            //var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FullName),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            var loginUser = new LoginUser()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                Dob = user.Dob,
                Id = user.Id,
                UserName = user.UserName,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RoleName = roles.Name
            };
            return new ApiSuccessResult<LoginUser>(loginUser);
        }

        public async Task<ApiResult<bool>> Register(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            var role = await _roleManager.FindByNameAsync("Client");
            if (user != null)
            {
                return new ApiErrorResult<bool>("Tài khoản đã tồn tại");
            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ApiErrorResult<bool>("Emai đã tồn tại");
            }

            user = new AppUser()
            {
                Dob = request.Dob,
                Email = request.Email,
                FullName = request.FullName,
                Gender = request.Gender,
                IdentityCard = request.IdentityCard,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                LocationID = request.LocationID,
                ActiveDate = DateTime.Now,
                AppRoleID = role.Id,
                Status = Data.Enums.Status.Active,
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>(result.Succeeded);
            }
            return new ApiErrorResult<bool>("Đăng ký không thành công");
        }

        public async Task<ApiResult<bool>> Delete(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("User không tồn tại");
            }
            var reult = await _userManager.DeleteAsync(user);
            if (reult.Succeeded)
                return new ApiSuccessResult<bool>();

            return new ApiErrorResult<bool>("Xóa không thành công");
        }

        public async Task<ApiResult<UserVm>> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<UserVm>("User không tồn tại");
            }
            var roles = await _roleManager.FindByIdAsync(user.AppRoleID.ToString());
            var broom = from br in _context.BookRooms select br;
            var userVm = new UserVm()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                Dob = user.Dob,
                Id = user.Id,
                UserName = user.UserName,
                IdentityCard = user.IdentityCard,
                Gender = user.Gender,
                Status = user.Status,
                ActiveDate = user.ActiveDate,
                Address = user.Address,
                Avatar = user.Avatar,
                No = user.No,
                LocationID = user.LocationID,
                Role = new RoleVm()
                {
                    RoleID = roles.Id,
                    Name = roles.Name,
                    Description = roles.Description
                },
                BookRooms = broom.Where(b => b.UserID == user.Id).Select(br => new BookRoomVm()
                {
                    BookRoomID = br.BookRoomID,
                    BookingDate = br.BookingDate,
                    UserID = user.Id,
                    AccommodationID = br.AccommodationID,
                    No = br.No,
                    Qty = br.Qty,
                    BookingUser = br.BookingUser,
                    FromDate = br.FromDate,
                    ToDate = br.ToDate,
                    CheckInIdentityCard = br.CheckInIdentityCard,
                    CheckInMail = br.CheckInMail,
                    CheckInName = br.CheckInName,
                    CheckInNote = br.CheckInNote,
                    Status = br.Status,
                    TotalPrice = br.TotalPrice
                }).ToList()
            };
            return new ApiSuccessResult<UserVm>(userVm);
        }

        public async Task<ApiResult<PagedResult<UserVm>>> GetUsersAllPaging(GetUserPagingRequest request)
        {
            var query = from u in _context.AppUsers
                        join r in _context.Roles on u.AppRoleID equals r.Id
                        join l in _context.Locations on u.LocationID equals l.LocationID
                        join b in _context.BookRooms on u.Id equals b.UserID into br
                        from b in br.DefaultIfEmpty()
                        select new { u, r ,b , l};

            var broom = from br in _context.BookRooms select br;

            //var patient = _context.Patients;

            if (!string.IsNullOrEmpty(request.SearchKey))
            {
                query = query.Where(x => x.u.UserName.Contains(request.SearchKey)
                 || x.u.PhoneNumber.Contains(request.SearchKey));
            }

            if (!string.IsNullOrEmpty(request.RoleID.ToString()))
            {
                query = query.Where(x => x.u.AppRoleID == request.RoleID);
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new UserVm()
                {
                    Email = x.u.Email,
                    PhoneNumber = x.u.PhoneNumber,
                    UserName = x.u.UserName,
                    Gender = x.u.Gender,
                    Id = x.u.Id,
                    FullName = x.u.FullName,
                    Status = x.u.Status,
                    Avatar = x.u.Avatar,
                    Dob = x.u.Dob,
                    ActiveDate = x.u.ActiveDate,
                    //LocationID = x.u.LocationID,
                    IdentityCard = x.u.IdentityCard,
                    Address = x.u.Address,
                    No = x.u.No,
                    Location = new LocationVm()
                    {
                        LocationID = x.l.LocationID,
                        Name = x.l.Name,
                        IsDeleted = x.l.IsDeleted,
                        Code = x.l.Code,
                        SortOrder = x.l.SortOrder,
                        ParentID = x.l.ParentID,
                        Type = x.l.Type
                    },
                    Role = new RoleVm()
                    {
                        RoleID = x.r.Id,
                        Description = x.r.Description,
                        Name = x.r.Name
                    } ,
                    BookRooms = broom.Where(b=>b.UserID == x.u.Id).Select(br => new BookRoomVm()
                    {
                        BookRoomID = br.BookRoomID,
                        BookingDate = br.BookingDate,
                        UserID = x.u.Id,
                        AccommodationID = br.AccommodationID,
                        No = br.No,
                        Qty = br.Qty,
                        BookingUser = br.BookingUser,
                        FromDate = br.FromDate,
                        ToDate = br.ToDate,
                        CheckInIdentityCard = br.CheckInIdentityCard,
                        CheckInMail = br.CheckInMail,
                        CheckInName = br.CheckInName,
                        CheckInNote = br.CheckInNote,
                        Status = br.Status,
                        TotalPrice = br.TotalPrice
                    }).ToList()
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<UserVm>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<UserVm>>(pagedResult);
        }
    }
}
