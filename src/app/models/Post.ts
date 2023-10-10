import { Reply } from "./Reply";

export interface Post {
    id: number, 
    board: string, 
    title: string, 
    author: string, 
    replies: number, 
    views: number, 
    body: string, 
    postReplies?: Reply[]
}