'use client';
import { useState, useEffect } from "react";
import Swal from 'sweetalert2';
import Loader from "../loader";
import NewProduct from "../newproduct";
import NewOrder from "../neworder";
import List from "../listproducts";

const API_URL = 'http://localhost:5000/api/v1/';


const getJwtToken = async () => {
    try {
        const tokenRequest = await fetch(`${API_URL}order/get-jwt`, {
            method: 'GET',
            headers: {
                "Content-Type": "application/json",
            }
        });

        if (!tokenRequest.ok) {
            throw new Error(`Error al obtener el token: ${tokenRequest.statusText}`);
        }

        const tokenResponse = await tokenRequest.json();
        return tokenResponse.token;
    } catch (error) {
        console.error("Error al obtener el token JWT:", error);
        throw error;
    }
};

const fetchData = async (url, method = 'GET', body = null) => {
    try {
        let token = await getJwtToken();
        const headers = {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`,
        };

        const response = await fetch(url, {
            method,
            headers,
            body: body ? JSON.stringify(body) : null,
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Error: ${response.status} ${response.statusText} - ${errorText}`);
        }

        return await response.json();
    } catch (error) {
        console.error("Error en la solicitud:", error);
        Swal.fire({
            icon: "error",
            title: "Error",
            text: error.message,
            customClass: {
                popup: 'dark:bg-gray-800 dark:text-white',
                confirmButton: 'bg-blue-500 hover:bg-blue-700 text-white',
            },
        });
        throw error;
    }
};


export default function Main() {
    const [loading, setLoading] = useState(true);
    const [refresh, setRefresh] = useState(true);
    const [products, setProducts] = useState([]);
    const [editing, setEditing] = useState(false);
    const [isProductModalOpen, setIsProductModalOpen] = useState(false);
    const [isOrderModalOpen, setIsOrderModalOpen] = useState(false);
    const [formData, setFormData] = useState({ name: "", description: "", price: "", stock: "" });
    const [orderData, setOrderData] = useState({ productId: "", quantity: 1, products: [] });

    useEffect(() => {
        const getProducts = async () => {
            try {
                const data = await fetchData(`${API_URL}product/get-all`);
                setProducts(data.data.products);
            } finally {
                setLoading(false);
            }
        };
        getProducts();
    }, [refresh]);

    const handleChange = (e) => setFormData(prev => ({ ...prev, [e.target.name]: e.target.value }));

    const handleOrderChange = (e) => setOrderData(prevData => ({ ...prevData, [e.target.name]: e.target.value }));

    const handleAddProductToOrder = () => {
        const { productId, quantity } = orderData;

        if (!productId || quantity <= 0) {
            Swal.fire({
                icon: "error",
                title: "Error",
                text: "Selecciona un producto y asegúrate de que la cantidad sea válida.",
                customClass: {
                    popup: 'dark:bg-gray-800 dark:text-white',
                    confirmButton: 'bg-blue-500 hover:bg-blue-700 text-white',
                },
            });
            return;
        }

        const selectedProduct = products.find(product => product.id === parseInt(productId));
        if (selectedProduct) {
            // Asegurarse de que prevData.products es un array antes de actualizar
            setOrderData(prevData => ({
                ...prevData,
                products: Array.isArray(prevData.products) ? [...prevData.products, { ...selectedProduct, quantity: parseInt(quantity) }] : [{ ...selectedProduct, quantity: parseInt(quantity) }],
                productId: "",
                quantity: 1
            }));
        }
    };

    const cleanProductData = () => setFormData({ name: "", description: "", price: "", stock: "" });

    const handleAddProduct = async (e) => {
        e.preventDefault();
        if (Object.values(formData).some(field => !field)) {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Revisa que todos los campos estén llenos!",
                customClass: {
                    popup: 'dark:bg-gray-800 dark:text-white',
                    confirmButton: 'bg-blue-500 hover:bg-blue-700 text-white',
                },
            });
            return;
        }

        const url = editing ? `${API_URL}product/update` : `${API_URL}product/create`;
        try {
            await fetchData(url, 'POST', formData);
            Swal.fire({
                icon: "success",
                title: `Registro ${editing ? 'actualizado' : 'creado'} con éxito`,
                showConfirmButton: true,
                customClass: {
                    popup: 'dark:bg-gray-800 dark:text-white',
                    confirmButton: 'bg-blue-500 hover:bg-blue-700 text-white',
                },
            });

            setEditing(false);
            cleanProductData();
            setIsProductModalOpen(false);
            setRefresh(!refresh);
        } catch (error) {
        }
    };

    const handleQuantityChange = (index, newQuantity) => {
        setOrderData(prevData => {
            const updatedProducts = [...prevData.products];
            updatedProducts[index].quantity = newQuantity;
            return {
                ...prevData,
                products: updatedProducts
            };
        });
    };

    const handleDelete = async (id) => {
        Swal.fire({
            title: "Eliminar registro?",
            text: "No podras revertir esto!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Eliminar",
            cancelButtonText: "Cancelar",
            customClass: {
                popup: 'dark:bg-gray-800 dark:text-white',
                confirmButton: 'bg-blue-500 hover:bg-blue-700 text-white',
            },
        }).then(async (result) => {
            if (result.isConfirmed) {
                try {
                    await fetchData(`${API_URL}product/delete/${id}`, 'DELETE');
                    Swal.fire({
                        icon: "success",
                        title: "Registro eliminado con éxito",
                        showConfirmButton: true,
                        customClass: {
                            popup: 'dark:bg-gray-800 dark:text-white',
                            confirmButton: 'bg-blue-500 hover:bg-blue-700 text-white',
                        },
                    });
                    setProducts(prev => prev.filter(product => product.id !== id));
                } catch (error) {
                }
            }
        });
    };

    const handleEdit = (product) => {
        setFormData({ ...product });
        setEditing(true);
        setIsProductModalOpen(true);
    };

    const closeModal = () => {
        setIsProductModalOpen(false);
        cleanProductData();
        setEditing(false);
    };

    const openOrderModal = () => {
        setOrderData({ productId: "", quantity: 1 });
        setIsOrderModalOpen(true);
    };

    const closeOrderModal = () => setIsOrderModalOpen(false);

    const handleCreateOrder = async (e) => {
        e.preventDefault();
        if (orderData.products.length === 0) {
            Swal.fire({
                icon: "error",
                title: "Error",
                text: "No has añadido productos a la orden.",
                customClass: {
                    popup: 'dark:bg-gray-800 dark:text-white',
                    confirmButton: 'bg-blue-500 hover:bg-blue-700 text-white',
                },
            });
            return;
        }

        const orderDetails = orderData.products.map(product => ({
            productId: product.id,
            quantity: product.quantity,
        }));

        try {
            let res = await fetchData(`${API_URL}order/create`, 'POST', { orderDetails });
            if (res.data.result.errors.length > 0) {
                Swal.fire({
                    icon: "error",
                    title: "Error",
                    text: res.data.result.errors[0],
                    customClass: {
                        popup: 'dark:bg-gray-800 dark:text-white',
                        confirmButton: 'bg-blue-500 hover:bg-blue-700 text-white',
                    },
                });
                return
            }
            Swal.fire({
                icon: "success",
                title: `Orden creada con éxito`,
                showConfirmButton: true,
                customClass: {
                    popup: 'dark:bg-gray-800 dark:text-white',
                    confirmButton: 'bg-blue-500 hover:bg-blue-700 text-white',
                },
            });
            setRefresh(!refresh);
            setOrderData({ productId: "", quantity: 1, products: [] });
            closeOrderModal();
        } catch (error) {
            console.error("Error al crear la orden:", error);
        }
    };

    return (
        <div className="min-h-screen bg-gray-900 text-white p-6">
            <div className="max-w-6xl mx-auto bg-gray-800 p-8 rounded-lg shadow-md">
                <h1 className="text-3xl font-bold text-center mb-6">Listado de productos</h1>
                {loading ? (
                    <Loader />
                ) : (
                    <>
                        <div className="flex justify-end mb-4">
                            <button onClick={() => setIsProductModalOpen(true)} className="bg-green-500 hover:bg-green-600 text-white font-bold py-2 px-4 rounded transition-all duration-300 ease-in-out transform hover:scale-105">
                                Añadir Producto
                            </button>
                            <button onClick={openOrderModal} className="bg-blue-500 hover:bg-blue-600 text-white font-bold py-2 px-4 rounded ml-4 transition-all duration-300 ease-in-out transform hover:scale-105">
                                Crear Orden
                            </button>
                        </div>
                        <List products={products} handleEdit={handleEdit} handleDelete={handleDelete} />
                    </>
                )}
            </div>

            {isProductModalOpen && <NewProduct handleAddProduct={handleAddProduct} formData={formData} editing={editing} handleChange={handleChange} closeModal={closeModal} />}
            {isOrderModalOpen && <NewOrder handleCreateOrder={handleCreateOrder} products={products} handleOrderChange={handleOrderChange} closeOrderModal={closeOrderModal} orderData={orderData} handleAddProductToOrder={handleAddProductToOrder} handleQuantityChange={handleQuantityChange} />}
        </div>
    );
}
