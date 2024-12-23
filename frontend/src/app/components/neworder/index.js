export default function NewOrder({ handleCreateOrder, products, handleOrderChange, closeOrderModal, orderData, handleAddProductToOrder, handleQuantityChange }) {
    return (
        <div className="fixed inset-0 flex items-center justify-center z-50">
            <div className="bg-white dark:bg-gray-800 p-6 rounded-lg w-96 shadow-lg">
                <h2 className="text-xl font-bold mb-4 text-gray-800 dark:text-white">Crear Orden</h2>
                <form onSubmit={handleCreateOrder}>
                    <div className="mb-4">
                        <label htmlFor="productId" className="block text-sm font-medium text-gray-800 dark:text-white">Selecciona un Producto</label>
                        <select
                            id="productId"
                            name="productId"
                            className="mt-2 p-2 border border-gray-300 dark:border-gray-600 rounded w-full bg-white dark:bg-gray-700 text-gray-800 dark:text-white"
                            value={orderData.productId}
                            onChange={handleOrderChange}
                        >
                            <option value="">Seleccionar producto</option>
                            {products.map((product) => (
                                <option key={product.id} value={product.id}>
                                    {product.name}
                                </option>
                            ))}
                        </select>
                    </div>

                    <div className="mb-4">
                        <label htmlFor="quantity" className="block text-sm font-medium text-gray-800 dark:text-white">Cantidad</label>
                        <input
                            type="number"
                            id="quantity"
                            name="quantity"
                            min="1"
                            className="mt-2 p-2 border border-gray-300 dark:border-gray-600 rounded w-full bg-white dark:bg-gray-700 text-gray-800 dark:text-white"
                            value={orderData.quantity}
                            onChange={handleOrderChange}
                        />
                    </div>

                    <div className="flex justify-between mb-4">
                        <button
                            type="button"
                            onClick={handleAddProductToOrder}
                            className="bg-green-500 hover:bg-green-600 text-white py-2 px-4 rounded"
                        >
                            Añadir a la orden
                        </button>
                    </div>

                    {orderData.products?.length > 0 && (
                        <div className="mt-4">
                            <h3 className="text-lg font-semibold text-gray-800 dark:text-white">Productos en la orden:</h3>
                            <ul className="mt-2">
                                {orderData.products.map((item, index) => (
                                    <li key={index} className="flex justify-between py-2 border-b border-gray-300 dark:border-gray-600">
                                        <span>{item.name}</span>
                                        <span>
                                            <input
                                                type="number"
                                                value={item.quantity}
                                                min="1"
                                                onChange={(e) => handleQuantityChange(index, e.target.value)}
                                                className="w-14 p-2 border rounded-lg bg-gray-700 text-white focus:outline-none focus:ring-2 focus:ring-blue-500 dark:bg-gray-800 dark:text-white dark:focus:ring-blue-600"

                                            />
                                            x ${item.price} = ${item.quantity * item.price}
                                        </span>
                                    </li>
                                ))}
                            </ul>
                        </div>
                    )}

                    <div className="flex justify-end mt-4">
                        <button
                            type="button"
                            onClick={closeOrderModal}
                            className="bg-gray-500 hover:bg-gray-600 text-white py-2 px-4 rounded mr-2"
                        >
                            Cancelar
                        </button>
                        <button
                            type="submit"
                            className="bg-blue-500 hover:bg-blue-600 text-white py-2 px-4 rounded"
                        >
                            Crear Orden
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
}
