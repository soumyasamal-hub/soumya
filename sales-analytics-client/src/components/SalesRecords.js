import React, { useState, useEffect } from 'react';
import { getSalesRecords, createSalesRecord, updateSalesRecord, deleteSalesRecord } from '../services/api';

function SalesRecords() {
  const [salesRecords, setSalesRecords] = useState([]);
  const [newRecord, setNewRecord] = useState({
    productName: '',
    quantity: 0,
    price: 0.0,
    saleDate: ''
  });

  useEffect(() => {
    fetchSalesRecords();
  }, []);

  const fetchSalesRecords = async () => {
    const response = await getSalesRecords();
    setSalesRecords(response.data);
  };

  const handleCreate = async () => {
    await createSalesRecord(newRecord);
    fetchSalesRecords();
  };

  const handleUpdate = async (id) => {
    await updateSalesRecord(id, newRecord);
    fetchSalesRecords();
  };

  const handleDelete = async (id) => {
    await deleteSalesRecord(id);
    fetchSalesRecords();
  };

  return (
    <div>
      <h1>Sales Records</h1>
      <input
        type="text"
        placeholder="Product Name"
        value={newRecord.productName}
        onChange={(e) => setNewRecord({ ...newRecord, productName: e.target.value })}
      />
      <input
        type="number"
        placeholder="Quantity"
        value={newRecord.quantity}
        onChange={(e) => setNewRecord({ ...newRecord, quantity: parseInt(e.target.value) })}
      />
      <input
        type="number"
        placeholder="Price"
        value={newRecord.price}
        onChange={(e) => setNewRecord({ ...newRecord, price: parseFloat(e.target.value) })}
      />
      <input
        type="date"
        value={newRecord.saleDate}
        onChange={(e) => setNewRecord({ ...newRecord, saleDate: e.target.value })}
      />
      <button onClick={handleCreate}>Add</button>

      <ul>
        {salesRecords.map((record) => (
          <li key={record.id}>
            {record.productName} - {record.quantity} - {record.price} - {record.saleDate}
            <button onClick={() => handleUpdate(record.id)}>Update</button>
            <button onClick={() => handleDelete(record.id)}>Delete</button>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default SalesRecords;
