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

const HumidityChart = ({ data }) => {
  // Preparing the data for the chart
  const chartData = {
    labels: data.map(item => item.time),
    datasets: [
      {
        label: 'Humidity',
        data: data.map(item => item.value),
        fill: false,
        backgroundColor: 'rgb(99,171,253)',
        borderColor: 'rgba(99, 171, 253, 0.5)',
      }
    ],
  };

  // Chart configuration
  const options = {
    responsive: true, // Make sure responsiveness is enabled
    maintainAspectRatio: false, // Maintain aspect ratio or set to false to fill container
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
        }
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
        }
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

  return (
    <div style={{ height: '125%', width: '100%' }}>
      <Line data={chartData} options={options} />
    </div>
  );
};

export default HumidityChart;