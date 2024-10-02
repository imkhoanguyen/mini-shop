export interface Image{
  id: number;
  url: File;
  isMain: boolean;
  publicId: string;
  variantId: number;
}
export interface ImageAdd{
  url: File;
  isMain: boolean;
  variantId: number;
}

