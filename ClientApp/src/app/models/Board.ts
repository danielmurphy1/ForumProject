import { Post } from "./Post";

export interface Board{
    id: number,
    title: string, 
    description: string, 
    topics: number,
    lastPost?: Post,
    imgUrl: string, 
    posts: Post[]

}