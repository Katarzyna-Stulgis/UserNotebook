export interface IUser {
    id: string,
    firstName: string,
    lastName: string,
    birthDate: Date,
    gender: string
    phoneNumber?: number | null,
    position?: string | null,
    shoeSize?: string | null
}