import { User } from './user'

export class Notifications {
    constructor() {
        this.messageNotifications = []
        this.gameNotifications = []
    }

    messageNotifications: MessageNotification[]
    gameNotifications: GameToken[]
}

export class MessageNotification {
    constructor(conversationId: number, unreadMessages: number) {
        this.conversationId = conversationId
        this.quantityOfUnreadMessages = unreadMessages
    }

    conversationId: number
    quantityOfUnreadMessages: number
}

export class GameToken {
    constructor(gameId: string, gameName: string, challengingUser: User, expirationDate: Date) {
        this.gameId = gameId
        this.gameName = gameName
        this.challengingUser = challengingUser
        this.expirationDate = expirationDate
    }

    gameId: string
    gameName: string
    challengingUser: User
    expirationDate: Date
}
