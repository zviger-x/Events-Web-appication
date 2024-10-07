export interface UserDTO {
    id: number | undefined,
    name: string,
    surname: string,
    birthDate: string | undefined,
    email: string,
    password: string,
    role: string,
}