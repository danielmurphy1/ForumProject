import { User } from "./User";

export interface Reply {
    id?: number, 
    postId: number,  
    user?: User,
    userId: number, 
    body: string, 
    createdAt: Date
}