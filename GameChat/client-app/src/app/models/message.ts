import { User } from './user';

export class Message {
    conversationId: number
    contents: string
    dateSent: Date
    sender: User
}
