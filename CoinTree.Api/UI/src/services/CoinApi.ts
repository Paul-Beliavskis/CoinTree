import axios from 'axios';

const getCoinStatsAsync = async (coin: String) => {
try{
    var itemList = await axios.get(`https://cointreetest.azurewebsites.net/api/coins/${coin}`);

    return itemList;
}catch(e)
    {
        //TODO: send back and error code so calling code can display the appropriate error toast to the user
        console.log("Failed to fetch items");
    }
return [{}];
}

const CoinApi = {
    getCoinStatsAsync: getCoinStatsAsync
}

export { getCoinStatsAsync };
