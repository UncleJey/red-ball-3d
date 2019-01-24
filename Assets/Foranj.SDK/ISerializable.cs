namespace Foranj.SDK {
	public interface ISerializable<T> {
		T Serialize();
		void Deserialize(T data);
	}
}