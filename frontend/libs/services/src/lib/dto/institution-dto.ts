import { AddressDto } from "./address-dto";
import { ClassifierDto } from "./classifier-dto";
import { InstitutionReplicationDto } from "./institution-replication-dto";
import { TranslationDto } from "./translation-dto";

export interface InstitutionDto {
  id?: number;
  name?: string;
  name_translation_code?: string;
  reg_code?: string;
  kmkr?: string;
  address_id?: number;
  type_classifier_id?: number;
  valid_from?: Date;
  valid_to?: Date;
  translations?: TranslationDto[];
  type_classifier?: ClassifierDto;
  replications?: InstitutionReplicationDto[];
  address?: AddressDto,
}
