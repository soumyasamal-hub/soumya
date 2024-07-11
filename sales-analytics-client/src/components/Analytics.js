import React, { useState, useEffect } from 'react';
import { getTotalSales, getTopProducts, getSalesTrends } from '../services/api';

function Analytics() {
  const [totalSales, setTotalSales] = useState(0);
  const [topProducts, setTopProducts] = useState([]);
  const [salesTrends, setSalesTrends] = useState([]);

  useEffect(() => {
    fetchAnalytics();
  }, []);

  const fetchAnalytics = async () => {
    const totalSalesResponse = await getTotalSales();
    setTotalSales(totalSalesResponse.data);

    const topProductsResponse = await getTopProducts();
    setTopProducts(topProductsResponse.data);

    const salesTrendsResponse = await getSalesTrends();
    setSalesTrends(salesTrendsResponse.data);
  };

  return (
    <div>
      <h1>Analytics</h1>
      <div>
        <h2>Total Sales: {totalSales}</h2>
      </div>
      <div>
        <h2>Top Products</h2>
        <ul>
          {topProducts.map((product) => (
            <li key={product.productName}>
              {product.productName} - {product.totalSales}
            </li>
          ))}
        </ul>
      </div>
      <div>
        <h2>Sales Trends</h2>
        <ul>
          {salesTrends.map((trend) => (
            <li key={trend.date}>
              {trend.date} - {trend.totalSales}
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
}

export default Analytics;
