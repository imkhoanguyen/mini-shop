import { Variant } from "./variant.module";
import { Image} from "./image.module";
import { Category } from "./category.module";

export interface Product
{
  id: number;
  name: string;
  description: string;
  created: Date;
  variants: Variant[];
  categoryIds: Category[];
  imageUrls: Image[];
  status: number;
}
