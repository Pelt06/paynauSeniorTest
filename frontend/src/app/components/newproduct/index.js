import React, { useState, useEffect } from "react";

export default function NewProduct({
  handleAddProduct, formData, editing, handleChange, closeModal
}) {


  return (
    <div className="fixed inset-0 flex items-center justify-center z-50 bg-black bg-opacity-50 animate-fade-in">
      <div className="bg-gray-800 p-8 rounded-lg shadow-lg max-w-lg w-full transform transition-transform duration-300 ease-out scale-95 animate-zoom-in">
        <h2 className="text-2xl font-bold mb-4">{editing ? "Editar Producto" : "Añadir Producto"}</h2>

        <form onSubmit={handleAddProduct} method="POST" className="space-y-4">
          <div>
            <label className="block text-gray-400 text-sm font-bold mb-2" htmlFor="name">Nombre</label>
            <input
              id="name"
              name="name"
              type="text"
              value={formData.name}
              onChange={handleChange}
              className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-900 leading-tight focus:outline-none focus:shadow-outline"
              placeholder="Introduce el nombre"
            />
          </div>

          <div>
            <label className="block text-gray-400 text-sm font-bold mb-2" htmlFor="description">Descripcion</label>
            <input
              id="description"
              name="description"
              type="text"
              value={formData.description}
              onChange={handleChange}
              className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-900 leading-tight focus:outline-none focus:shadow-outline"
              placeholder="Introduce la descripcion"
            />
          </div>

          <div>
            <label className="block text-gray-400 text-sm font-bold mb-2" htmlFor="price">Precio</label>
            <input
              id="price"
              name="price"
              type="text"
              value={formData.price}
              onChange={handleChange}
              className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-900 leading-tight focus:outline-none focus:shadow-outline"
              placeholder="Introduce el precio"
            />
          </div>

          <div>
            <label className="block text-gray-400 text-sm font-bold mb-2" htmlFor="stock">Stock</label>
            <input
              id="stock"
              name="stock"
              type="stock"
              value={formData.stock}
              onChange={handleChange}
              className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-900 leading-tight focus:outline-none focus:shadow-outline"
              placeholder="Introduce el stock"
            />
          </div>
          <div className="flex justify-between">
            <button
              type="submit"
              className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
            >
              {editing ? "Actualizar" : "Añadir"}
            </button>
            <button
              type="button"
              onClick={closeModal}
              className="bg-gray-500 hover:bg-gray-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
            >
              Cancelar
            </button>
          </div>
        </form>
      </div>
    </div>
  )
}
