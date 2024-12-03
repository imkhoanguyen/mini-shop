using Shop.Domain.Entities;
using Shop.Domain.Enum;

namespace Shop.Infrastructure.DataAccess.Seed
{
    public class ProductSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if (context.Products.Any())
            {
                return;
            }
            var products = new List<Product>{

                //Product 1
                new Product { 
                    Name = "Plain Double-Breasted Trench Jacket", 
                    Description = @"Material:	100% Polyester",
                    Status = ProductStatus.Public,

                    ProductCategories = new List<ProductCategory> {
                        new ProductCategory { 
                            CategoryId = 2,
                            ProductId = 1,
                        }
                    },
                    Image = new ProductImage {
                       ImgUrl = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733203364/XXL_p0191264264_rujkso.webp",
                    },
                    Variants = new List<Variant> {
                        new Variant {
                            ColorId = 5,
                            SizeId = 2,
                            Price = 50.00m,
                            PriceSell = 49.59m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733203665/XXL_p0191264582_im4be1.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 5,
                            SizeId = 3,
                            Price = 50.00m,
                            PriceSell = 49.59m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733203665/XXL_p0191264582_im4be1.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 5,
                            SizeId = 4,
                            Price = 50.00m,
                            PriceSell = 49.59m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733203665/XXL_p0191264582_im4be1.webp",
                                }
                            }
                        }
                    },
                },
                //Product 2
                new Product {
                    Name = "V-Neck Cable Knit Sweater",
                    Description = @"Material: 100% Polyester",
                    Status = ProductStatus.Public,

                    ProductCategories = new List<ProductCategory> {
                        new ProductCategory {
                            CategoryId = 3,
                            ProductId = 2,
                        }
                    },
                    Image = new ProductImage {
                       ImgUrl = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733204323/XXL_p0188260583_jeo1gq.webp",
                    },
                    Variants = new List<Variant> {
                        new Variant {
                            ColorId = 6,
                            SizeId = 3,
                            Price = 22.00m,
                            PriceSell = 21.40m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733204460/XXL_p0188261322_jlumrx.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 9,
                            SizeId = 2,
                            Price = 22.00m,
                            PriceSell = 21.40m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733204565/XXL_p0188261323_ati0ep.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 2,
                            SizeId = 2,
                            Price = 22.00m,
                            PriceSell = 21.40m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733204323/XXL_p0188260583_jeo1gq.webp",
                                }
                            }
                        }
                    },

                },
                //Product 3
                new Product {
                    Name = "Collar Striped Sweater",
                    Description = @"Material: 95% Polyester",
                    Status = ProductStatus.Public,

                    ProductCategories = new List<ProductCategory> {
                        new ProductCategory {
                            CategoryId = 3,
                            ProductId = 3,
                        }
                    },
                    Image = new ProductImage {
                       ImgUrl = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733204889/XXL_p0214363169_kmk46i.webp",
                    },
                    Variants = new List<Variant> {
                        new Variant {
                            ColorId = 5,
                            SizeId = 1,
                            Price = 37.98m,
                            PriceSell = 37.00m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733204889/XXL_p0214363169_kmk46i.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 10,
                            SizeId = 2,
                            Price = 37.98m,
                            PriceSell = 37.00m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733204912/XXL_p0214363489_ajujhx.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 2,
                            SizeId = 1,
                            Price = 37.98m,
                            PriceSell = 37.00m,
                            Quantity = 15,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733204323/XXL_p0188260583_jeo1gq.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 2,
                            SizeId = 3,
                            Price = 37.98m,
                            PriceSell = 37.00m,
                            Quantity = 20,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733204323/XXL_p0188260583_jeo1gq.webp",
                                }
                            }
                        }
                    },

                },
                //Product 4
                new Product {
                    Name = "Short-Sleeve Pocket Detail Shirt",
                    Description = @"Material: 95%Polyester  5%Elastane  ",
                    Status = ProductStatus.Public,

                    ProductCategories = new List<ProductCategory> {
                        new ProductCategory {
                            CategoryId = 1,
                            ProductId = 4,
                        }
                    },
                    Image = new ProductImage {
                       ImgUrl = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733205252/XXL_p0179409691_incmz2.webp",
                    },
                    Variants = new List<Variant> {
                        new Variant {
                            ColorId = 5,
                            SizeId = 2,
                            Price = 13.84m,
                            PriceSell = 9.00m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733205284/XXL_p0179409864_giauly.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 11,
                            SizeId = 2,
                            Price = 13.84m,
                            PriceSell = 9.00m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733205302/XXL_p0179409862_eat1is.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 6,
                            SizeId = 2,
                            Price = 13.84m,
                            PriceSell = 9.00m,
                            Quantity = 15,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733205343/XXL_p0179409863_dixwqu.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 5,
                            SizeId = 3,
                            Price = 13.84m,
                            PriceSell = 9.00m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733205284/XXL_p0179409864_giauly.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 11,
                            SizeId = 3,
                            Price = 13.84m,
                            PriceSell = 9.00m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733205302/XXL_p0179409862_eat1is.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 6,
                            SizeId = 3,
                            Price = 13.84m,
                            PriceSell = 9.00m,
                            Quantity = 15,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733205343/XXL_p0179409863_dixwqu.webp",
                                }
                            }
                        },

                    },

                },
                //Product 5
                new Product {
                    Name = "Short-Sleeve Contrast Stitched Button-Up Shirt",
                    Description = @"Material: 90%Polyester   10%Elastane  ",
                    Status = ProductStatus.Public,

                    ProductCategories = new List<ProductCategory> {
                        new ProductCategory {
                            CategoryId = 1,
                            ProductId = 5,
                        }
                    },
                    Image = new ProductImage {
                       ImgUrl = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733205729/XXL_p0209158513_f0akjy.webp",
                    },
                    Variants = new List<Variant> {
                        new Variant {
                            ColorId = 5,
                            SizeId = 1,
                            Price = 37.98m,
                            PriceSell = 37.00m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733205729/XXL_p0209158513_f0akjy.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 10,
                            SizeId = 2,
                            Price = 37.98m,
                            PriceSell = 37.00m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733205804/XXL_p0209158769_xqq46z.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 11,
                            SizeId = 2,
                            Price = 37.98m,
                            PriceSell = 37.00m,
                            Quantity = 15,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733205811/XXL_p0209158773_l4nhex.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 6,
                            SizeId = 2,
                            Price = 37.98m, 
                            PriceSell = 37.00m,
                            Quantity = 20,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733204323/XXL_p0188260583_jeo1gq.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 10,
                            SizeId = 3,
                            Price = 37.98m, 
                            PriceSell = 37.00m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733205804/XXL_p0209158769_xqq46z.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 11,
                            SizeId = 3,
                            Price = 37.98m, 
                            PriceSell = 37.00m,
                            Quantity = 15,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733205811/XXL_p0209158773_l4nhex.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 6,
                            SizeId = 3,
                            Price = 37.98m, 
                            PriceSell = 37.00m,
                            Quantity = 20,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733204323/XXL_p0188260583_jeo1gq.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 10,
                            SizeId = 4,
                            Price = 37.98m, 
                            PriceSell = 37.00m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733205804/XXL_p0209158769_xqq46z.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 11,
                            SizeId = 4,
                            Price = 37.98m, 
                            PriceSell = 37.00m,
                            Quantity = 15,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733205811/XXL_p0209158773_l4nhex.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 6,
                            SizeId = 4,
                            Price = 37.98m, 
                            PriceSell = 37.00m,
                            Quantity = 20,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733204323/XXL_p0188260583_jeo1gq.webp",
                                }
                            }
                        }
                    },

                },
                //Product 6
                new Product {
                    Name = "Cinched Sleeve Square Neck Plain Ruched Tie Front Blouse",
                    Description = @"Material: 95%Polyester 5%Elastane",
                    Status = ProductStatus.Public,

                    ProductCategories = new List<ProductCategory> {
                        new ProductCategory {
                            CategoryId = 4,
                            ProductId = 6,
                        }
                    },
                    Image = new ProductImage {
                       ImgUrl = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733206044/XXL_p0193320884_hklxcw.webp",
                    },
                    Variants = new List<Variant> {
                        new Variant {
                            ColorId = 5,
                            SizeId = 1,
                            Price = 15.45m,
                            PriceSell = 10.05m,
                            Quantity = 25,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733206044/XXL_p0193320884_hklxcw.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 10,
                            SizeId = 1,
                            Price = 15.45m,
                            PriceSell = 10.05m,
                            Quantity = 40,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733206050/XXL_p0193321125_gqktum.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 5,
                            SizeId = 2,
                            Price = 15.45m,
                            PriceSell = 10.05m,
                            Quantity = 25,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733206044/XXL_p0193320884_hklxcw.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 10,
                            SizeId = 2,
                            Price = 15.45m,
                            PriceSell = 10.05m,
                            Quantity = 40,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733206050/XXL_p0193321125_gqktum.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 5,
                            SizeId = 3,
                            Price = 15.45m,
                            PriceSell = 10.05m,
                            Quantity = 25,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733206044/XXL_p0193320884_hklxcw.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 10,
                            SizeId = 3,
                            Price = 15.45m,
                            PriceSell = 10.05m,
                            Quantity = 40,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733206050/XXL_p0193321125_gqktum.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 5,
                            SizeId = 4,
                            Price = 15.45m,
                            PriceSell = 10.05m,
                            Quantity = 25,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733206044/XXL_p0193320884_hklxcw.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 10,
                            SizeId = 4,
                            Price = 15.45m,
                            PriceSell = 10.05m,
                            Quantity = 40,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733206050/XXL_p0193321125_gqktum.webp",
                                }
                            }
                        },
                    },

                },
                //Product 7
                new Product {
                    Name = "Plain Blazer + Double-Breasted Vest + Straight Leg Dress Pants",
                    Description = @"Material:	68% Polyester, 28% , Viscose, 4% PU Fiber
                                    Color:	Set of 3 - Blazer & Vest & Dress Pants - Black",
                    Status = ProductStatus.Public,

                    ProductCategories = new List<ProductCategory> {
                        new ProductCategory {
                            CategoryId = 5,
                            ProductId = 7,
                        }
                    },
                    Image = new ProductImage {
                       ImgUrl = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733208976/XXL_p0191066133_sdpf1c.webp",
                    },
                    Variants = new List<Variant> {
                        new Variant {
                            ColorId = 2,
                            SizeId = 2,
                            Price = 54.29m,
                            PriceSell = 54.20m,
                            Quantity = 25,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733208993/XXL_p0191067967_kdzvrt.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 5,
                            SizeId = 2,
                            Price = 54.29m,
                            PriceSell = 54.20m,
                            Quantity = 40,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733209018/XXL_p0191066133_gnmkli.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 1,
                            SizeId = 2,
                            Price = 54.29m,
                            PriceSell = 54.20m,
                            Quantity = 25,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733209085/XXL_p0191067969_vhkoea.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 6,
                            SizeId = 2,
                            Price = 54.29m,
                            PriceSell = 54.20m,
                            Quantity = 25,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733209100/XXL_p0191067966_slqmoe.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 2,
                            SizeId = 3,
                            Price = 54.29m,
                            PriceSell = 54.20m,
                            Quantity = 25,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733208993/XXL_p0191067967_kdzvrt.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 5,
                            SizeId = 3,
                            Price = 54.29m,
                            PriceSell = 54.20m,
                            Quantity = 40,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733209018/XXL_p0191066133_gnmkli.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 1,
                            SizeId = 3,
                            Price = 54.29m,
                            PriceSell = 54.20m,
                            Quantity = 25,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733209085/XXL_p0191067969_vhkoea.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 6,
                            SizeId = 3,
                            Price = 54.29m,
                            PriceSell = 54.20m,
                            Quantity = 25,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733209100/XXL_p0191067966_slqmoe.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 2,
                            SizeId = 4,
                            Price = 54.29m,
                            PriceSell = 54.20m,
                            Quantity = 25,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733208993/XXL_p0191067967_kdzvrt.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 5,
                            SizeId = 4,
                            Price = 54.29m,
                            PriceSell = 54.20m,
                            Quantity = 40,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733209018/XXL_p0191066133_gnmkli.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 1,
                            SizeId = 4,
                            Price = 54.29m,
                            PriceSell = 54.20m,
                            Quantity = 25,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733209085/XXL_p0191067969_vhkoea.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 6,
                            SizeId = 4,
                            Price = 54.29m,
                            PriceSell = 54.20m,
                            Quantity = 25,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733209100/XXL_p0191067966_slqmoe.webp",
                                }
                            }
                        },
                    }
                },
                //Product 8
                new Product {
                    Name = "Stand Collar Two Tone Hood Zip Puffer",
                    Description = @"Material: 95%Polyester 5%Elastane",
                    Status = ProductStatus.Public,

                    ProductCategories = new List<ProductCategory> {
                        new ProductCategory {
                            CategoryId = 2,
                            ProductId = 8,
                        },
                        new ProductCategory
                        {
                            CategoryId = 6,
                            ProductId = 8,
                        }
                    },
                    Image = new ProductImage {
                       ImgUrl = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733209564/XXL_p0212478055_mijhuo.webp",
                    },
                    Variants = new List<Variant> {
                        new Variant {
                            ColorId = 5,
                            SizeId = 1,
                            Price = 48.88m,
                            PriceSell = 39.11m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733209564/XXL_p0212478055_mijhuo.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 10,
                            SizeId = 1,
                            Price = 48.88m,
                            PriceSell = 39.11m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733209623/XXL_p0212478443_yyiigg.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 5,
                            SizeId = 2,
                            Price = 48.88m,
                            PriceSell = 39.11m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733209564/XXL_p0212478055_mijhuo.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 10,
                            SizeId = 2,
                            Price = 48.88m,
                            PriceSell = 39.11m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733209623/XXL_p0212478443_yyiigg.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 5,
                            SizeId = 3,
                            Price = 48.88m,
                            PriceSell = 39.11m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733209564/XXL_p0212478055_mijhuo.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 10,
                            SizeId = 3,
                            Price = 48.88m,
                            PriceSell = 39.11m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733209623/XXL_p0212478443_yyiigg.webp",
                                }
                            }
                        },
                    },

                },
                //Product 9
                new Product {
                    Name = "Stand Collar Two Tone Hood Zip Puffer Jacket",
                    Description = @"Material: 95% Polyester",
                    Status = ProductStatus.Public,

                    ProductCategories = new List<ProductCategory> {
                        new ProductCategory {
                            CategoryId = 2,
                            ProductId = 9,
                        },
                        new ProductCategory {
                            CategoryId = 6,
                            ProductId = 9,
                        }
                    },
                    Image = new ProductImage {
                       ImgUrl = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733209848/XXL_p0213443708_y6zc0c.webp",
                    },
                    Variants = new List<Variant> {
                        new Variant {
                            ColorId = 11,
                            SizeId = 1,
                            Price = 39.59m,
                            PriceSell = 39.50m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733209848/XXL_p0213443708_y6zc0c.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 6,
                            SizeId = 1,
                            Price = 39.59m,
                            PriceSell = 39.50m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733209871/XXL_p0213443709_hxbr4z.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 4,
                            SizeId = 1,
                            Price = 39.59m,
                            PriceSell = 39.50m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733209878/XXL_p0213443710_ehvwce.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 11,
                            SizeId = 2,
                            Price = 39.59m,
                            PriceSell = 39.50m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733209848/XXL_p0213443708_y6zc0c.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 6,
                            SizeId = 2,
                            Price = 39.59m,
                            PriceSell = 39.50m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733209871/XXL_p0213443709_hxbr4z.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 4,
                            SizeId = 2,
                            Price = 39.59m,
                            PriceSell = 39.50m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733209878/XXL_p0213443710_ehvwce.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 11,
                            SizeId = 3,
                            Price = 39.59m,
                            PriceSell = 39.50m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733209848/XXL_p0213443708_y6zc0c.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 6,
                            SizeId = 3,
                            Price = 39.59m,
                            PriceSell = 39.50m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733209871/XXL_p0213443709_hxbr4z.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 4,
                            SizeId = 3,
                            Price = 39.59m,
                            PriceSell = 39.50m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733209878/XXL_p0213443710_ehvwce.webp",
                                }
                            }
                        },
                    }
                },
                //Product 10
                new Product {
                    Name = "Elbow-Sleeve Crewneck Cat Print T-Shirt",
                    Description = @"Material: 95% Polyester
                                    Wash care: Hand Wash, Machine Wash",
                    Status = ProductStatus.Public,

                    ProductCategories = new List<ProductCategory> {
                        new ProductCategory {
                            CategoryId = 1,
                            ProductId = 10,
                        }
                    },
                    Image = new ProductImage {
                       ImgUrl = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733210236/XXL_p0209112854_lbeew8.webp",
                    },
                    Variants = new List<Variant> {
                        new Variant {
                            ColorId = 5,
                            SizeId = 2,
                            Price = 15.55m,
                            PriceSell = 9.33m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733210236/XXL_p0209112854_lbeew8.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 6,
                            SizeId = 2,
                            Price = 15.55m,
                            PriceSell = 9.33m,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733210264/XXL_p0209112844_kcpxmx.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 1,
                            SizeId = 2,
                            Price = 15.55m,
                            PriceSell = 9.33m,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733210269/XXL_p0209112849_c3j8nc.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 5,
                            SizeId = 3,
                            Price = 15.55m,
                            PriceSell = 9.33m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733210236/XXL_p0209112854_lbeew8.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 6,
                            SizeId = 3,
                            Price = 15.55m,
                            PriceSell = 9.33m,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733210264/XXL_p0209112844_kcpxmx.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 1,
                            SizeId = 3,
                            Price = 15.55m,
                            PriceSell = 9.33m,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733210269/XXL_p0209112849_c3j8nc.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 5,
                            SizeId = 4,
                            Price = 15.55m,
                            PriceSell = 9.33m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733210236/XXL_p0209112854_lbeew8.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 6,
                            SizeId = 4,
                            Price = 15.55m,
                            PriceSell = 9.33m,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733210264/XXL_p0209112844_kcpxmx.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 1,
                            SizeId = 4,
                            Price = 15.55m,
                            PriceSell = 9.33m,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733210269/XXL_p0209112849_c3j8nc.webp",
                                }
                            }
                        },
                    }
                },
                //Product 11
                new Product {
                    Name = "Elbow-Sleeve Crew Neck Goose Print T-Shirt",
                    Description = @"Material: 100% Cotton",
                    Status = ProductStatus.Public,

                    ProductCategories = new List<ProductCategory> {
                        new ProductCategory {
                            CategoryId = 1,
                            ProductId = 11,
                        }
                    },
                    Image = new ProductImage {
                       ImgUrl = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733210586/XXL_p0196637096_s7bafv.webp",
                    },
                    Variants = new List<Variant> {
                        new Variant {
                            ColorId = 6,
                            SizeId = 1,
                            Price = 12.73m,
                            PriceSell = 12.50m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733210586/XXL_p0196637096_s7bafv.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 3,
                            SizeId = 1,
                            Price = 12.73m,
                            PriceSell = 12.50m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733210605/XXL_p0196637260_yp74ex.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 2,
                            SizeId = 1,
                            Price = 12.73m,
                            PriceSell = 12.50m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733210617/XXL_p0196637248_zmvupe.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 8,
                            SizeId = 1,
                            Price = 12.73m,
                            PriceSell = 12.50m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733210645/XXL_p0196637252_o1ygb9.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 6,
                            SizeId = 2,
                            Price = 12.73m,
                            PriceSell = 12.50m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733210586/XXL_p0196637096_s7bafv.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 3,
                            SizeId = 2,
                            Price = 12.73m,
                            PriceSell = 12.50m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733210605/XXL_p0196637260_yp74ex.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 2,
                            SizeId = 2,
                            Price = 12.73m,
                            PriceSell = 12.50m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733210617/XXL_p0196637248_zmvupe.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 8,
                            SizeId = 2,
                            Price = 12.73m,
                            PriceSell = 12.50m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733210645/XXL_p0196637252_o1ygb9.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 6,
                            SizeId = 3,
                            Price = 12.73m,
                            PriceSell = 12.50m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733210586/XXL_p0196637096_s7bafv.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 3,
                            SizeId = 3,
                            Price = 12.73m,
                            PriceSell = 12.50m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733210605/XXL_p0196637260_yp74ex.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 2,
                            SizeId = 3,
                            Price = 12.73m,
                            PriceSell = 12.50m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733210617/XXL_p0196637248_zmvupe.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 8,
                            SizeId = 3,
                            Price = 12.73m,
                            PriceSell = 12.50m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733210645/XXL_p0196637252_o1ygb9.webp",
                                }
                            }
                        },
                    }
                },
                //Product 12
                new Product {
                    Name = "V-Neck Button-Up Cardigan + Scarf",
                    Description = @"Material:	100% Polyester Camel",
                    Status = ProductStatus.Public,

                    ProductCategories = new List<ProductCategory> {
                        new ProductCategory {
                            CategoryId = 6,
                            ProductId = 12,
                        },
                        new ProductCategory {
                            CategoryId = 7,
                            ProductId = 12,
                        }
                    },
                    Image = new ProductImage {
                       ImgUrl = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733211084/XXL_p0212221425_lbeu1d.webp",
                    },
                    Variants = new List<Variant> {
                        new Variant {
                            ColorId = 1,
                            SizeId = 1,
                            Price = 14.12m,
                            PriceSell = 14.00m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733211084/XXL_p0212221425_lbeu1d.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 9,
                            SizeId = 1,
                            Price = 14.12m,
                            PriceSell = 14.00m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733211090/XXL_p0212221827_yi6ayx.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 2,
                            SizeId = 1,
                            Price = 14.12m,
                            PriceSell = 14.00m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733211115/XXL_p0212221831_ixztlq.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 1,
                            SizeId = 2,
                            Price = 14.12m,
                            PriceSell = 14.00m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733211084/XXL_p0212221425_lbeu1d.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 9,
                            SizeId = 2,
                            Price = 14.12m,
                            PriceSell = 14.00m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733211090/XXL_p0212221827_yi6ayx.webp",
                                }
                            }
                        },
                        new Variant {
                            ColorId = 2,
                            SizeId = 2,
                            Price = 14.12m,
                            PriceSell = 14.00m,
                            Quantity = 50,
                            Status = VariantStatus.Public,
                            Images = new List<VariantImage> {
                                new VariantImage {
                                   ImgUrl  = "https://res.cloudinary.com/dlhwuvhhp/image/upload/v1733211115/XXL_p0212221831_ixztlq.webp",
                                }
                            }
                        }
                    },

                },

            };
            await context.Products.AddRangeAsync(products);
        }
    }
}