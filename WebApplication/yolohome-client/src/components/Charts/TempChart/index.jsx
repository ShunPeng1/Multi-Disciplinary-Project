import React from 'react';
import { Line } from 'react-chartjs-2';
import { Chart as ChartJS, CategoryScale, LinearScale, PointElement, LineElement, Title, Tooltip, Legend } from 'chart.js';

// Registering the necessary components for Chart.js
ChartJS.register(
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend
);

const TemperatureChart = ({ data }) => {
  // Preparing the data for the chart
  const chartData = {
    labels: data.map(item => item.time),  // Assuming 'time' is your label
    datasets: [
      {
        label: 'Temperature',
        data: data.map(item => item.value),
        fill: false,
        backgroundColor: 'rgb(255,123,66)',
        borderColor: 'rgba(255, 123, 66, 0.5)',
      }
    ],
  };

  // Chart configuration
  const options = {
    responsive: true, // Make sure responsiveness is enabled
    maintainAspectRatio: true, // Maintain aspect ratio or set to false to fill container
    scales: {
      x: {
        beginAtZero: true,
        grid: { 
          color: 'rgba(255, 255, 255, 0.5)', // Sets the color of the x-axis grid lines
          drawBorder: true,
          drawOnChartArea: true,
          drawTicks: true,
        },
        ticks: {
          color: 'white'
        },
      },
      y: {
        beginAtZero: true,
        grid: { 
          color: 'rgba(255, 255, 255, 0.5)', // Sets the color of the x-axis grid lines
          drawBorder: true,
          drawOnChartArea: true,
          drawTicks: true,
        },
        ticks: {
          color: 'white'
        },
      }
    },
    plugins: {
      legend: {
        display: true,
        position: 'bottom',
        labels: {
          color: 'white'
        }
      }
    },
    animation: {
      duration: 0 // General animation time
    },
    hover: {
      animationDuration: 0 // Duration of animations when hovering an item
    },
    responsiveAnimationDuration: 0 // Animation duration after a resize
  };

  return <Line data={chartData} options={options} />;
};

export default TemperatureChart;
