namespace SomeOrderThing.Tests
{
    using Xbehave;

    public class CookTests
    {
        private IMidget midget;

        [Background]
        public void Background()
        {
            "Given a message publisher"
                .f(() =>
                {
                    messagesPublished = new List<object>();
                    messagePublisher = A.Fake<IPublisher>();

                    A.CallTo(() => messagePublisher.Publish(A<object>.Ignored))
                        .Invokes(call =>
                        {
                            var message = (call.Arguments.First());
                            messagesPublished.Add(message);
                        });
                });

            "And a process manager"
                .f(() => midget = new LithuanianMidget(messagePublisher));

            "And an instrument ID"
                .f(() => instrumentId = Guid.NewGuid());
        }

        [Scenario]
        public void RetryCooking(IMidget midget)
        {
            "Given an initial price"
                .f(() => initialPrice = 10);

            "Given an second price"
                .f(() => secondPrice = 20);

            "Given an second price"
                .f(() => thirdPrice = 15);

            "When I acquire a position with that initial price"
                .f(() => this.processorManager.Handle(new PositionAcquired { InstrumentId = instrumentId, Price = initialPrice }));

            "And I get a price update"
                .f(() => this.processorManager.Handle(new PriceUpdated { InstrumentId = instrumentId, Price = secondPrice }));

            "And the windows for the initial price are triggered"
                .f(() =>
                {
                    this.processorManager.Handle(new RemoveFrom10sWindow
                    {
                        InstrumentId = instrumentId,
                        Price = initialPrice
                    });
                    this.processorManager.Handle(new RemoveFrom13sWindow
                    {
                        InstrumentId = instrumentId,
                        Price = initialPrice
                    });
                });

            "And the windows for the second price are triggered"
                .f(() =>
                {
                    this.processorManager.Handle(new RemoveFrom10sWindow { InstrumentId = instrumentId, Price = secondPrice });
                    this.processorManager.Handle(new RemoveFrom13sWindow { InstrumentId = instrumentId, Price = secondPrice });
                });

            "And I get another price update"
                .f(() => this.processorManager.Handle(new PriceUpdated { InstrumentId = instrumentId, Price = thirdPrice }));

            "And the 10s windows for the third price is triggered"
                .f(() => this.processorManager.Handle(new RemoveFrom10sWindow { InstrumentId = instrumentId, Price = thirdPrice }));

            "And I clear the published messages"
                .f(() => this.messagesPublished.Clear());

            "And I get message to remove from the 13s window"
                .f(() => this.processorManager.Handle(new RemoveFrom13sWindow { InstrumentId = instrumentId, Price = thirdPrice }));

            "Then a message is published to flag the stop loss price as being hit"
                .f(() =>
                {
                    var message = (StopLossHit)this.messagesPublished[0];
                    message.InstrumentId.Should().Be(this.instrumentId);
                });
        }
    }
}
