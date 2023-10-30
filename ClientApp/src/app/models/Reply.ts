import { User } from "./User";

export interface Reply {
    id: number, 
    originalPost: string, 
    title: string, 
    user: User, 
    body: string
}