import {SourceType} from "./source-type";

export interface NewSavedPasswordRequest {
  password: string;
  source: string;
  sourceType: SourceType;
}
