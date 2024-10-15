using API.DTOs;
using Microsoft.AspNetCore.Identity;
using System.Security.AccessControl;

namespace API.Constains
{
    public class ClaimStore
    {
        // color
        public const string Color_View = "Color.View";
        public const string Color_Create = "Color.Create";
        public const string Color_Edit = "Color.Edit";
        public const string Color_Delete = "Color.Delete";

        // category
        public const string Category_View = "Category.View";
        public const string Category_Create = "Category.Create";
        public const string Category_Edit = "Category.Edit";
        public const string Category_Delete = "Category.Delete";

        // size
        public const string Size_View = "Size.View";
        public const string Size_Create = "Size.Create";
        public const string Size_Edit = "Size.Edit";
        public const string Size_Delete = "Size.Delete";

        // product
        public const string Product_View = "Product.View";
        public const string Product_Create = "Product.Create";
        public const string Product_Edit = "Product.Edit";
        public const string Product_Delete = "Product.Delete";

        // order
        public const string Order_ComfirmPayment = "Order.ConfirmPayment";
        public const string Order_Comfirm = "Order.Confirm";
        public const string Order_Delete = "Order.Delete";
        public const string Order_View = "Order.View";


        //shipping
        public const string Shipping_Create = "Shipping.Create";
        public const string Shipping_Delete = "Shipping.Delete";
        public const string Shipping_Edit = "Shipping.Delete";
        public const string Shipping_View = "Shipping.View";


        // user management
        public const string User_UpdateUserRole = "User.UpdateUserRole";
        public const string User_Lockout = "User.Lockout";

        // role management
        public const string Role_Create = "Role.Create";
        public const string Role_Edit = "Role.Edit";
        public const string Role_Delete = "Role.Delete";
        public const string Role_View = "Role.View";




        public static List<IdentityRoleClaim<string>> adminClaims = new List<IdentityRoleClaim<string>>()
        {

            // color 
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Color_Create},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Color_Edit},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Color_Delete},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Color_View},


            // category
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Category_Create},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Category_Edit},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Category_Delete},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Category_View},


            // size
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Size_Create},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Size_Edit},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Size_Delete},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Size_View},


            // product
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Product_Create},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Product_Edit},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Product_Delete},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Product_View},


            // order
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Order_Comfirm},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Order_ComfirmPayment},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Order_Delete},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Order_View},


            // shipping
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Shipping_Create},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Shipping_Delete},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Shipping_Edit},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Shipping_View},


            // user management
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=User_UpdateUserRole},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=User_Lockout},

            // role management
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Role_Create},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Role_Edit},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Role_Delete},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Role_View},


        };


        public static List<PermissionGroupDto> AllPermissionGroups = new List<PermissionGroupDto>() {
            new PermissionGroupDto
            {
                GroupName = "Quản lý sản phẩm",
                Permissions = new List<PermissionItemDto>
                {
                    new PermissionItemDto {Name = "Tạo sản phẩm", ClaimValue = Product_Create},
                    new PermissionItemDto {Name = "Sửa sản phẩm", ClaimValue = Product_Edit},
                    new PermissionItemDto {Name = "Xóa sản phẩm", ClaimValue = Product_Delete},
                    new PermissionItemDto {Name = "Xem sản phẩm", ClaimValue = Product_View},
                }
            },

            new PermissionGroupDto
            {
                GroupName = "Quản lý danh mục",
                Permissions = new List<PermissionItemDto>
                {
                    new PermissionItemDto {Name = "Tạo danh mục", ClaimValue = Category_Create},
                    new PermissionItemDto {Name = "Sửa danh mục", ClaimValue = Category_Edit},
                    new PermissionItemDto {Name = "Xóa danh mục", ClaimValue = Category_Delete},
                    new PermissionItemDto {Name = "Xem danh mục", ClaimValue = Category_View},

                }
            },

            new PermissionGroupDto
            {
                GroupName = "Quản lý màu sắc",
                Permissions = new List<PermissionItemDto>
                {
                    new PermissionItemDto {Name = "Tạo màu sắc", ClaimValue = Color_Create},
                    new PermissionItemDto {Name = "Sửa màu sắc", ClaimValue = Color_Edit},
                    new PermissionItemDto {Name = "Xóa màu sắc", ClaimValue = Color_Delete},
                    new PermissionItemDto {Name = "Xem màu sắc", ClaimValue = Color_View},
                }
            },

            new PermissionGroupDto
            {
                GroupName = "Quản lý size",
                Permissions = new List<PermissionItemDto>
                {
                    new PermissionItemDto {Name = "Tạo Size", ClaimValue = Size_Create},
                    new PermissionItemDto {Name = "Sửa Size", ClaimValue = Size_Edit},
                    new PermissionItemDto {Name = "Xóa Size", ClaimValue = Size_Delete},
                    new PermissionItemDto {Name = "Xem Size", ClaimValue = Size_View},
                }
            },

            new PermissionGroupDto
            {
                GroupName = "Quản lý quyền",
                Permissions = new List<PermissionItemDto>
                {
                    new PermissionItemDto {Name = "Tạo Quyền", ClaimValue = Role_Create},
                    new PermissionItemDto {Name = "Sửa Quyền", ClaimValue = Role_Edit},
                    new PermissionItemDto {Name = "Xóa Quyền", ClaimValue = Role_Delete},
                    new PermissionItemDto {Name = "Xem Quyền", ClaimValue = Role_View}
                }
            },

            new PermissionGroupDto
            {
                GroupName = "Quản lý người dùng",
                Permissions = new List<PermissionItemDto>
                {
                    new PermissionItemDto {Name = "Thay đổi quyền", ClaimValue = User_UpdateUserRole},
                    new PermissionItemDto {Name = "Khóa tài khoản", ClaimValue = User_Lockout},
                }
            },

            new PermissionGroupDto
            {
                GroupName = "Quản lý đặt hàng",
                Permissions = new List<PermissionItemDto>
                {
                    new PermissionItemDto {Name = "Xác nhận thanh toán", ClaimValue = Order_ComfirmPayment},
                    new PermissionItemDto {Name = "Xác nhận đơn hàng", ClaimValue = Order_Comfirm},
                    new PermissionItemDto {Name = "Xóa đơn hàng", ClaimValue = Order_Delete},
                }
            },

            new PermissionGroupDto
            {
                GroupName = "Quản lý phương thức vận chuyển",
                Permissions = new List<PermissionItemDto>
                {
                    new PermissionItemDto {Name = "Tạo phương thức vận chuyển", ClaimValue = Shipping_Create},
                    new PermissionItemDto {Name = "Sửa phương thức vận chuyển", ClaimValue = Shipping_Edit},
                    new PermissionItemDto {Name = "Xóa phương thức vận chuyển", ClaimValue = Shipping_Delete},
                }
            },
        };
    }
}
