import { Post } from "./Post";

export interface Board{
    id: number,
    title: string, 
    description: string, 
    topics: number,
    lastTitle: string,
    lastUser: string,
    lastDate: string,
    imgUrl: string, 
    posts: Post[]

}