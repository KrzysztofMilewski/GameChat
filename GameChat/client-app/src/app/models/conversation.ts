import { User } from './user'

export class Conversation {
    id: number
    title: string
    participants: User[]
    unreadMessages: number
    dateOfLastMessage: Date
}
