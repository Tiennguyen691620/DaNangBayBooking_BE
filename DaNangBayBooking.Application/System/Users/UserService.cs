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
using DaNangBayBooking.Utilities.Extensions;
using DaNangBayBooking.Data.Enums;

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
            if (user.Status == false)
            {
                return new ApiErrorResult<LoginUser>("Tài khoản bị vô hiệu hóa");
            }

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
            if(user.Status == false)
            {
                return new ApiErrorResult<LoginUser>("Tài khoản bị vô hiệu hóa");
            } 

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
            string year = DateTime.Now.ToString("yy");
            int count = await _context.Users.Where(x => x.No.Contains("CUSTOMER-" + year)).CountAsync();
            string str = "";
            if (count < 9) str = "CUSTOMER-" + DateTime.Now.ToString("yy") + "-000" + (count + 1);
            else if (count < 99) str = "CUSTOMER-" + DateTime.Now.ToString("yy") + "-00" + (count + 1);
            else if (count < 999) str = "CUSTOMER-" + DateTime.Now.ToString("yy") + "-0" + (count + 1);
            else if (count < 9999) str = "CUSTOMER-" + DateTime.Now.ToString("yy") + "-" + (count + 1);

            var role = await _roleManager.FindByNameAsync("Client");
            if (await _userManager.FindByNameAsync(request.PhoneNumber) != null)
            {
                return new ApiErrorResult<bool>("Số điện thoại đã tồn tại");
            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ApiErrorResult<bool>("Emai đã tồn tại");
            }

            var user = new AppUser()
            {
                Id = request.Id,
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Dob = request.Dob.FromUnixTimeStamp(),
                IdentityCard = request.IdentityCard,
                Gender = request.Gender,
                UserName = str,
                No = str,
                ActiveDate = DateTime.Now,
                Status = true,
                AppRoleID = role.Id,
                LocationID = request.SubDistrict.LocationID
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return new ApiSuccessResult<bool>(false);
            }
            return new ApiSuccessResult<bool>(true);
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

            var sd = await _context.Locations.FindAsync(user.LocationID);
            var d = await _context.Locations.FindAsync(sd.ParentID);
            var p = await _context.Locations.FindAsync(d.ParentID);

            var roles = await _roleManager.FindByIdAsync(user.AppRoleID.ToString());
            var broom = from br in _context.BookRooms select br;
            var userVm = new UserVm()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                Dob = user.Dob.ToSecondsTimestamp(),
                Id = user.Id,
                UserName = user.UserName,
                IdentityCard = user.IdentityCard,
                Gender = user.Gender,
                //Status = user.Status.ToDictionaryItemDto<Data.Enums.Status>(),
                ActiveDate = user.ActiveDate.ToSecondsTimestamp(),
                Address = user.Address,
                Avatar = user.Avatar,
                No = user.No,
                Province = new LocationProvince()
                {
                    LocationID = p.LocationID,
                    Name = p.Name,
                    IsDeleted = p.IsDeleted,
                    ParentID = p.ParentID,
                    Code = p.Code,
                    Type = p.Type,
                    SortOrder = p.SortOrder
                },
                District = new LocationDistrict()
                {
                    LocationID = d.LocationID,
                    Name = d.Name,
                    IsDeleted = d.IsDeleted,
                    ParentID = d.ParentID,
                    Code = d.Code,
                    Type = d.Type,
                    SortOrder = d.SortOrder
                },
                SubDistrict = new LocationSubDistrict()
                {
                    LocationID = sd.LocationID,
                    Name = sd.Name,
                    IsDeleted = sd.IsDeleted,
                    ParentID = sd.ParentID,
                    Code = sd.Code,
                    Type = sd.Type,
                    SortOrder = sd.SortOrder
                },
                //LocationID = user.LocationID,
                Role = new RoleVm()
                {
                    RoleID = roles.Id,
                    Name = roles.Name,
                    Description = roles.Description
                },
            };
            return new ApiSuccessResult<UserVm>(userVm);
        }

        public async Task<ApiResult<PagedResult<UserVm>>> GetCustomerAllPaging(GetUserPagingRequest request)
        {
            var query = from u in _context.AppUsers
                        join r in _context.Roles on u.AppRoleID equals r.Id
                        join sd in _context.Locations on u.LocationID equals sd.LocationID
                        join d in _context.Locations on sd.ParentID equals d.LocationID
                        join p in _context.Locations on d.ParentID equals p.LocationID
                        join b in _context.BookRooms on u.Id equals b.UserID into br
                        from b in br.DefaultIfEmpty()
                        where r.Name.ToUpper() == "CLIENT"
                        select new { u, r , sd, d, p, b };

            var broom = from br in _context.BookRooms select br;

            //var patient = _context.Patients;

            if (!string.IsNullOrEmpty(request.SearchKey))
            {
                query = query.Where(x => x.u.FullName.Contains(request.SearchKey)
                 || x.u.PhoneNumber.Contains(request.SearchKey) || x.u.Email.Contains(request.SearchKey));
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
                    Avatar = x.u.Avatar,
                    Status = x.u.Status,
                    Dob = x.u.Dob.ToSecondsTimestamp(),
                    ActiveDate = x.u.ActiveDate.ToSecondsTimestamp(),
                    IdentityCard = x.u.IdentityCard,
                    Address = x.u.Address,
                    No = x.u.No,
                    Province = new LocationProvince()
                    {
                        LocationID = x.p.LocationID,
                        Name = x.p.Name,
                        IsDeleted = x.p.IsDeleted,
                        ParentID = x.p.ParentID,
                        Code = x.p.Code,
                        Type = x.p.Type,
                        SortOrder = x.p.SortOrder
                    },
                    District = new LocationDistrict()
                    {
                        LocationID = x.d.LocationID,
                        Name = x.d.Name,
                        IsDeleted = x.d.IsDeleted,
                        ParentID = x.d.ParentID,
                        Code = x.d.Code,
                        Type = x.d.Type,
                        SortOrder = x.d.SortOrder
                    },
                    SubDistrict = new LocationSubDistrict()
                    {
                        LocationID = x.sd.LocationID,
                        Name = x.sd.Name,
                        IsDeleted = x.sd.IsDeleted,
                        ParentID = x.sd.ParentID,
                        Code = x.sd.Code,
                        Type = x.sd.Type,
                        SortOrder = x.sd.SortOrder
                    },
                    Role = new RoleVm()
                    {
                        RoleID = x.r.Id,
                        Description = x.r.Description,
                        Name = x.r.Name
                    } ,
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

        public async Task<ApiResult<PagedResult<UserVm>>> GetAdminAllPaging(GetUserPagingRequest request)
        {
            var query = from u in _context.AppUsers
                        join r in _context.Roles on u.AppRoleID equals r.Id
                        join sd in _context.Locations on u.LocationID equals sd.LocationID
                        join d in _context.Locations on sd.ParentID equals d.LocationID
                        join p in _context.Locations on d.ParentID equals p.LocationID
                        //join s in _context.Status on u.Status equals s.Key
                        join b in _context.BookRooms on u.Id equals b.UserID into br
                        from b in br.DefaultIfEmpty()
                        where r.Name.ToUpper() == "ADMIN"
                        select new { u, r, b, sd, d, p };

            var broom = from br in _context.BookRooms select br;

            //var patient = _context.Patients;

            if (!string.IsNullOrEmpty(request.SearchKey))
            {
                query = query.Where(x => x.u.FullName.Contains(request.SearchKey)
                 || x.u.PhoneNumber.Contains(request.SearchKey) || x.u.Email.Contains(request.SearchKey));
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
                    Dob = x.u.Dob.ToSecondsTimestamp(),
                    ActiveDate = x.u.ActiveDate.ToSecondsTimestamp(),
                    IdentityCard = x.u.IdentityCard,
                    Address = x.u.Address,
                    No = x.u.No,
                    Province = new LocationProvince()
                    {
                        LocationID = x.p.LocationID,
                        Name = x.p.Name,
                        IsDeleted = x.p.IsDeleted,
                        ParentID = x.p.ParentID,
                        Code = x.p.Code,
                        Type = x.p.Type,
                        SortOrder = x.p.SortOrder
                    },
                    District = new LocationDistrict()
                    {
                        LocationID = x.d.LocationID,
                        Name = x.d.Name,
                        IsDeleted = x.d.IsDeleted,
                        ParentID = x.d.ParentID,
                        Code = x.d.Code,
                        Type = x.d.Type,
                        SortOrder = x.d.SortOrder
                    },
                    SubDistrict = new LocationSubDistrict()
                    {
                        LocationID = x.sd.LocationID,
                        Name = x.sd.Name,
                        IsDeleted = x.sd.IsDeleted,
                        ParentID = x.sd.ParentID,
                        Code = x.sd.Code,
                        Type = x.sd.Type,
                        SortOrder = x.sd.SortOrder
                    },
                    Role = new RoleVm()
                    {
                        RoleID = x.r.Id,
                        Description = x.r.Description,
                        Name = x.r.Name
                    },
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

        public async Task<ApiResult<bool>> CreateAdmin(CreateAdminRequest request)
        {
            string year = DateTime.Now.ToString("yy");
            int count = await _context.Users.Where(x => x.No.Contains("USER-" + year)).CountAsync();
            string str = "";
            if (count < 9) str = "USER-" + DateTime.Now.ToString("yy") + "-00" + (count + 1);
            else if (count < 99) str = "USER-" + DateTime.Now.ToString("yy") + "-0" + (count + 1);
            else if (count < 999) str = "USER-" + DateTime.Now.ToString("yy") + "-" + (count + 1);

            var role = await _roleManager.FindByNameAsync("Admin");
            

            var user = new AppUser()
            {
                Id = request.Id,
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Dob = request.Dob.FromUnixTimeStamp(),
                Address = request.Address,
                IdentityCard = request.IdentityCard,
                Gender = request.Gender,
                Avatar = request.Avatar,
                LocationID = request.SubDistrict.LocationID,
                UserName = str,
                No = str,
                ActiveDate = DateTime.Now,
                Status = true,
                AppRoleID = role.Id,
            };
            var result = await _userManager.CreateAsync(user, "123456789Abc@");
            if (!result.Succeeded)
            {
                return new ApiSuccessResult<bool>(false);
            }
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<bool>> UpdateStatusAdmin(Guid UserAdminID, bool Status)
        {
            var checkStatus = await _context.AppUsers.FindAsync(UserAdminID);
            if (checkStatus == null)
            {
                return new ApiSuccessResult<bool>(false);
            }
            if (checkStatus.Status == true)
            {
                checkStatus.Status = false;
            }
            else
            {
                checkStatus.Status = true;
            }
            //checkStatus.Status = Status;
            var result = await _context.SaveChangesAsync();
            if (result != 0)
            {
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiSuccessResult<bool>(false);
        }

        public async Task<ApiResult<bool>> UpdateStatusClient(Guid UserClientID, bool Status)
        {
            var checkStatus = await _context.AppUsers.FindAsync(UserClientID);
            if (checkStatus == null)
            {
                return new ApiSuccessResult<bool>(false);
            }
            if (checkStatus.Status == true)
            {
                checkStatus.Status = false;
            }
            else
            {
                checkStatus.Status = true;
            }
            //checkStatus.Status = Status;
            var result = await _context.SaveChangesAsync();
            if (result != 0)
            {
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiSuccessResult<bool>(false);
        }
    }
}
