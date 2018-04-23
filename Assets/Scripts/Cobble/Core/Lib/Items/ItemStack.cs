namespace Cobble.Core.Lib.Items {
    public class ItemStack {
        public Item Item;
        public int Amount = 0;

        public bool IsEmpty {
            get { return Item == null || Amount == 0;  }
        }

        public ItemStack(Item item, int amount = 0) {
            Item = item;
            Amount = amount;
        }
    }
}