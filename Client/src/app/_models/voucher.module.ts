export interface Voucher {
    id: number;
    title: string;
    description: string;
    percentage: number;       // Use 'number' for percentage values
    start_date: Date;
    end_date: Date;
    is_active: boolean;       // Use 'boolean' for active/inactive status
    created_at: Date;
    updated_at: Date;
  }
  
