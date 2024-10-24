export interface Role {
  id?: string;
  name: string;
  description: string;
  created?: string;
}

export interface Permission {
  claimValue: string;
  name: string;
}

export interface PermissionGroup {
  groupName: string;
  permissions: Permission[];
}
