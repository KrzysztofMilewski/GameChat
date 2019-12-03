export class Notifications {
    constructor() {
        this.messageNotifications = []
        this.gameNotifications = []
    }

    messageNotifications: MessageNotification[]
    gameNotifications: GameNotification[]
}

export class MessageNotification {
    constructor(conversationId: number, unreadMessages: number) {
        this.conversationId = conversationId
        this.quantityOfUnreadMessages = unreadMessages
    }

    conversationId: number
    quantityOfUnreadMessages: number
}

export class GameNotification {
    constructor(gameName: string, challengingUserId: number, expirationDate: Date) {
        this.gameName = gameName
        this.challengingUserId = challengingUserId
        this.expirationDate = expirationDate
    }

    gameName: string
    challengingUserId: number
    expirationDate: Date
}
