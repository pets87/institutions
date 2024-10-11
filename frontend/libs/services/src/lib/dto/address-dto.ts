export interface AddressDto {
  id: number;
  country: string;
  county: string;
  city: string;
  street: string;
  house: string;
  apartment?: string;
  postal_code: string;
  address_text: string;
}
