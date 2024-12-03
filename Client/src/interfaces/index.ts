export interface UserRegisterVM {
  Name: string
  Email: string
  Password: string
}

export interface UserLoginVM {
  Email: string
  Password: string
}

export interface UserSessionVM {
  Id: number
  Email: string
  Name: string
  Photo: string
  Works: WorkVM[]
  Token: string
}

export interface AttachmentVM {
  Id: number
  PublicationId: number
  UploadedBy: number
  Base64: string
  Url: string
  FileType: string
  Alt: string
}

export interface PublicationVM {
  Id: number
  CreatedBy: LightUserVM
  CreatedAt: string
  Enterprise: EnterpriseVM
  Ranking: number
  Information: string
  Attachments: AttachmentVM[]
  Comments: CommentVM[]
}

export interface EnterpriseVM {
  Id: number
  Name: string
}

export interface LightUserVM {
  Id: number
  Name: string
  Photo: string
}

export interface WorkVM {
  Id: number
  Enterprise: EnterpriseVM
  UserId: number
  StartDate: string
  EndDate: string | undefined | null
}

export interface CommentVM {
  Id: number
  PublicationId: number
  CreatedBy: LightUserVM
  CreatedAt: string
  Message: string
}
