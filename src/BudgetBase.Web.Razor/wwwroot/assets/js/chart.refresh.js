async function refreshDashboard() {
    try {
        // counters
        const inc = await getMetric('dashboard?handler=GetIncome', 'dashboard-income');
        const exp = await getMetric('dashboard?handler=GetExpenses', 'dashboard-expenses');
        await getMetric('dashboard?handler=GetTransfers', 'dashboard-transfers');
        setMetricValue('dashboard-net', inc - exp);
        await getMetric('dashboard?handler=GetUncategorized', 'dashboard-uncategorized');

        // P&L
        await getPLData();
        await getPieData();
        await getRecurringData();

        // 50/30/20
        setSimChart(inc);
    } catch (error) {
        console.error(error);
        // Handle any errors, e.g., update the UI to show an error message
    }
}

refreshDashboard();