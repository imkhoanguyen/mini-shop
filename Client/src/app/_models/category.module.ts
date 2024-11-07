export interface CategoryBase {
  name: string;
}

export interface CategoryAdd extends CategoryBase {}

export interface CategoryUpdate extends CategoryBase {
  id: number;
}

export interface CategoryDto extends CategoryBase {
  id: number;
  created: Date;
  updated: Date;
}
