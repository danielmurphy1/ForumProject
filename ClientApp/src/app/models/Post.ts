import { Reply } from "./Reply";

export interface Post {
    id: number, 
    boardId: number,
    board: string, 
    title: string, 
    user: string, 
    replies: number, 
    views: number, 
    body: string,
    createdAt: Date, 
    postReplies?: Reply[]
}