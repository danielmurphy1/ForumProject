import { Reply } from "./Reply";
import { User } from "./User";

export interface Post {
    id?: number, 
    boardId: number,
    board?: string, 
    title: string, 
    user?: User, 
    userId: number,
    replies: number, 
    views: number, 
    body: string,
    createdAt?: Date, 
    replyMessages?: Reply[]
}