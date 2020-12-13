import React, { useState, useEffect } from 'react';
import axios from 'axios'
import { Table, Row, Col, Spinner } from 'react-bootstrap';

import LeagueSummary from './LeagueSummary'
import NewTeam from './NewTeam'
import MatchStatus from './MatchStatus'
import SearchTeam from './SearchTeam'

import { routeUrl } from '../config/navigator';

const Home = () => {

	const title = 'League Dashboard';

	const Headline = ({ value }) => {
		return <div className='league-header'><h1>{value}</h1></div>;
	};

	const [leagueSummary, setLeagueSummary] = useState([]);
	const [loading, setLoading] = useState(false);

	const getLeagueSummary = ({ pageNumber, teamName }) => {
		let url = teamName ? `${routeUrl.url}/summary?pageNumber=${pageNumber}&teamName=${teamName.toLowerCase()}`
			: `${routeUrl.url}/summary?pageNumber=${pageNumber}`
		setLoading(true);
		axios.get(url)
			.then((response) => {
				const data = response.data;
				setLeagueSummary(data);
				setLoading(false);
			})
			.catch(error => { console.log(`Error: ${error}`); setLoading(false); });
	}

	useEffect(() => {
		getLeagueSummary({ pageNumber: 1 });
	}, []);




	return (
		<>
			<Headline value={title} />
			<Row>
				{loading && <Spinner animation="border" />}
				{leagueSummary.summary  &&
					<>
						<LeagueSummary
							summary={leagueSummary.summary}
							nextPage={leagueSummary.nextPage}
							previousPage={leagueSummary.previousPage}
							totalPage={leagueSummary.totalPage}
							getLeagueSummary={getLeagueSummary} /> </> }
				<Col className='right-container'>
					<NewTeam />
					<MatchStatus getLeagueSummary={getLeagueSummary}/>
				</Col>
			</Row>
		</>
	);
};

export default Home;