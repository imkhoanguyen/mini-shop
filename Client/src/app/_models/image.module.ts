export interface Image{
  id: number;
  url: File;
  isMain: boolean;
  publicId: string;
  productId: number;
}
export interface ImageAdd{
  url: File;
  isMain: boolean;
  productId: number;
}

