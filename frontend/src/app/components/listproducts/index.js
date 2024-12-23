import React, { useState, useEffect } from "react";


export default function List({ products, handleEdit, handleDelete }) {


  return (
    <div>
      <table className="min-w-full table-auto text-white">
        <thead>
          <tr className="bg-gray-700">
            <th className="px-4 py-2">Nombre</th>
            <th className="px-4 py-2">Descripcion</th>
            <th className="px-4 py-2">Precio</th>
            <th className="px-4 py-2">Stock</th>
            <th className="px-4 py-2">Acciones</th>
          </tr>
        </thead>
        <tbody>
          {products.length === 0 ? (
            <tr>
              <td colSpan="5" className="text-center text-gray-400">No hay productas registradas.</td>
            </tr>
          ) : (
            products.map((product) => (
              <tr key={product.id} className="border-b border-gray-600">
                <td className="px-4 py-2">{product.name}</td>
                <td className="px-4 py-2">{product.description}</td>
                <td className="px-4 py-2">{product.price}</td>
                <td className="px-4 py-2">{product.stock}</td>
                <td className="px-4 py-2 flex space-x-2">
                  <button
                    onClick={() => handleEdit(product)}
                    className="bg-yellow-500 hover:bg-yellow-600 text-white font-bold py-1 px-2 rounded transition-all duration-300 ease-in-out transform hover:scale-105"
                  >
                    Editar
                  </button>
                  <button
                    onClick={() => handleDelete(product.id)}
                    className="bg-red-500 hover:bg-red-600 text-white font-bold py-1 px-2 rounded transition-all duration-300 ease-in-out transform hover:scale-105"
                  >
                    Eliminar
                  </button>
                </td>
              </tr>
            ))
          )}
        </tbody>
      </table>
    </div>
  )
}
