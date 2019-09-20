import { User } from './user';

export class Message {
    id: number
    conversationId: number
    contents: string
    dateSent: Date
    sender: User
}
