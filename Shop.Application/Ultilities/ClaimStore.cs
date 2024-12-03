using Microsoft.AspNetCore.Identity;
using Shop.Application.DTOs.Auth.Permissions;

namespace Shop.Application.Ultilities
{
    public class ClaimStore
    {
        // color
        public const string Color_Create = "Color.Create";
        public const string Color_Edit = "Color.Edit";
        public const string Color_Delete = "Color.Delete";

        // category
        public const string Category_Create = "Category.Create";
        public const string Category_Edit = "Category.Edit";
        public const string Category_Delete = "Category.Delete";

        // size
        public const string Size_Create = "Size.Create";
        public const string Size_Edit = "Size.Edit";
        public const string Size_Delete = "Size.Delete";

        // product
        public const string Product_Create = "Product.Create";
        public const string Product_Edit = "Product.Edit";
        public const string Product_Delete = "Product.Delete";

        // order
        public const string Order_ComfirmPayment = "Order.ConfirmPayment";
        public const string Order_Comfirm = "Order.Confirm";


        //shipping
        public const string Shipping_Create = "Shipping.Create";
        public const string Shipping_Delete = "Shipping.Delete";
        public const string Shipping_Edit = "Shipping.Edit";


        // user management
        public const string User_UpdateUserRole = "User.UpdateUserRole";
        public const string User_Lock = "User.Lock";
        public const string User_Add = "User.Add";
        public const string User_Update = "User.Update";

        // role management
        public const string Role_Create = "Role.Create";
        public const string Role_Edit = "Role.Edit";
        public const string Role_Delete = "Role.Delete";
        public const string Change_Permission = "Role.ChangePermission";

        public const string Message_Reply = "Message.Reply";

        //discount
        public const string Discount_Create = "Discount.Create";
        public const string Discount_Delete = "Discount.Delete";
        public const string Discount_Edit = "Discount.Edit";

        // access admin page
        public const string Access_Admin = "Access.Admin";


        public static List<IdentityRoleClaim<string>> adminClaims = new List<IdentityRoleClaim<string>>()
        {

            // color 
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Color_Create},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Color_Edit},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Color_Delete},


            // category
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Category_Create},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Category_Edit},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Category_Delete},


            // size
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Size_Create},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Size_Edit},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Size_Delete},


            // product
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Product_Create},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Product_Edit},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Product_Delete},


            // order
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Order_Comfirm},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Order_ComfirmPayment},


            // shipping
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Shipping_Create},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Shipping_Delete},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Shipping_Edit},

             // discount
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Discount_Create},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Discount_Delete},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Discount_Edit},


            // user management
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=User_UpdateUserRole},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=User_Lock},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=User_Add},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=User_Update},


            // role management
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Role_Create},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Role_Edit},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Role_Delete},
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue=Change_Permission},


            // message management
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue= Message_Reply},

            

            // access page
            new IdentityRoleClaim<string> {ClaimType="Permission", ClaimValue= Access_Admin},
           
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
                }
            },

            new PermissionGroupDto
            {
                GroupName = "Quản lý kích thước",
                Permissions = new List<PermissionItemDto>
                {
                    new PermissionItemDto {Name = "Tạo Size", ClaimValue = Size_Create},
                    new PermissionItemDto {Name = "Sửa Size", ClaimValue = Size_Edit},
                    new PermissionItemDto {Name = "Xóa Size", ClaimValue = Size_Delete},
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
                    new PermissionItemDto {Name = "Thay đổi chức năng", ClaimValue = Change_Permission}
                }
            },

            new PermissionGroupDto
            {
                GroupName = "Quản lý người dùng",
                Permissions = new List<PermissionItemDto>
                {
                    new PermissionItemDto {Name = "Thay đổi quyền", ClaimValue = User_UpdateUserRole},
                    new PermissionItemDto {Name = "Khóa tài khoản", ClaimValue = User_Lock},
                    new PermissionItemDto {Name = "Thêm người dùng", ClaimValue = User_Add},
                    new PermissionItemDto {Name = "Thay đổi thông tin người dùng", ClaimValue = User_Update},
                }
            },

            new PermissionGroupDto
            {
                GroupName = "Quản lý đặt hàng",
                Permissions = new List<PermissionItemDto>
                {
                    new PermissionItemDto {Name = "Xác nhận thanh toán", ClaimValue = Order_ComfirmPayment},
                    new PermissionItemDto {Name = "Xác nhận đơn hàng", ClaimValue = Order_Comfirm},
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

            new PermissionGroupDto
            {
                GroupName = "Quản lý khuyến mãi",
                Permissions = new List<PermissionItemDto>
                {
                    new PermissionItemDto {Name = "Tạo khuyến mãi", ClaimValue = Discount_Create},
                    new PermissionItemDto {Name = "Sửa khuyến mãi", ClaimValue = Discount_Edit},
                    new PermissionItemDto {Name = "Xóa khuyến mãi", ClaimValue = Discount_Delete},
                }
            },

            new PermissionGroupDto
            {
                GroupName = "Quản lý tin nhắn",
                Permissions = new List<PermissionItemDto>
                {
                    new PermissionItemDto {Name = "Phản hồi tin nhắn từ khách hàng", ClaimValue = Message_Reply},
                }
            },

            new PermissionGroupDto
            {
                GroupName = "Quyền truy cập",
                Permissions = new List<PermissionItemDto>
                {
                    new PermissionItemDto {Name = "Quản lý danh mục", ClaimValue = Access_Admin},

                }
            },
        };
    }
}
