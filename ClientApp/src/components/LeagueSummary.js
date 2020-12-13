import React from 'react';
import { Table, Row, Col, Pagination } from 'react-bootstrap';

import SearchTeam from './SearchTeam'

const LeagueSummary = ({ summary, nextPage, previousPage, totalPage, getLeagueSummary }) => {

	const tableHeader = ['Name', 'Matches', 'Wins', 'Losses', 'Ties', 'Scores']

	const renderTableHeader = () => {
		return tableHeader.map((key, index) => {
			return <th key={index}>{key}</th>
		})
	}

	const renderTableData = () => {
		return summary.map((data, index) => {
			const { id, name, matchesPlayed, wins, losses, ties, scores } = data
			return (
				<tr key={id}>
					<td>{name}</td>
					<td>{matchesPlayed}</td>
					<td>{wins}</td>
					<td>{losses}</td>
					<td>{ties}</td>
					<td>{scores}</td>
				</tr>
			)
		})
	}

	const renderTable = () => {
		if (summary.length > 0) {
			<>
				<Row>
					<Col>
						<Table>
							<thead>
								<tr>
									{renderTableHeader()}
								</tr>
							</thead>
							<tbody>
								{summary.summary.length > 0 && renderTableData()}
							</tbody>
						</Table>
					</Col>
				</Row>
			</>
		}
		else {
			return (<h3>Not yet</h3>)
		}
	}

	const onPagination = (control) => {
		if (control == 'back' && previousPage != 0) {
			getLeagueSummary({ pageNumber: previousPage });
		}
		if (control == 'next' && nextPage <= totalPage) {
			getLeagueSummary({ pageNumber: nextPage });
		}
	}

	return (
		<>
			<Col>
				<SearchTeam getLeagueSummary={getLeagueSummary} />
				<Table>
					<thead>
						<tr>
							{renderTableHeader()}
						</tr>
					</thead>
					<tbody>
						{renderTableData()}
					</tbody>
				</Table>
				<Pagination>
					<Pagination.Prev disabled={previousPage == 0} onClick={() => onPagination('back')}>Back</Pagination.Prev>
					<Pagination.Next disabled={nextPage >= totalPage} onClick={() => onPagination('next')}>Next</Pagination.Next>
				</Pagination>
			</Col>
		</>
	)


};

export default LeagueSummary