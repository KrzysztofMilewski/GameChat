export class Notifications {
    constructor() {
        this.messageNotifications = []
    }

    messageNotifications: MessageNotification[]
}

export class MessageNotification {
    constructor(conversationId: number, unreadMessages: number) {
        this.conversationId = conversationId
        this.quantityOfUnreadMessages = unreadMessages
    }

    conversationId: number
    quantityOfUnreadMessages: number
}
