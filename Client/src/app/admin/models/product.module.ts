import { Variant } from "./variant.module";
import { Image} from "./image.module";
import { Category } from "./category.module";

export interface Product
{
  id: number;
  name: string;
  description: string;
  variants: Variant[];
  categoryId: Category[];
  images: Image[];
}
