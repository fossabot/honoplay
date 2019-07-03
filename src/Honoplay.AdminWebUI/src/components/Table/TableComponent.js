import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import {Table, TableBody, TableCell, 
        TablePagination, TableRow, Paper, 
        Checkbox, MuiThemeProvider} from '@material-ui/core';

import TableMenu from './TableMenu';
import EnhancedTableHead from './EnhancedTableHead';
import EnhancedTableToolbar from './EnhancedTableToolbar';

import { Style, theme} from './Style';


class TableComponent extends React.Component {
  constructor(props) {
        super(props);
        this.state = {
            selected: [],
            data: props.data,
            columns: props.columns,
            page: 0,
            rowsPerPage: 5,
          };
        this.handleChangePage=this.handleChangePage.bind(this);
        this.handleChangeRowsPerPage=this.handleChangeRowsPerPage.bind(this);
  }

  handleSelectAllClick (event)  {
    if (event.target.checked) {
        this.setState(state => ({ selected: state.data.map(n => n.id) }));
        return;
    }
    this.setState({ selected: [] });
  };

  handleClick (id) {
    const { selected } = this.state;
    const selectedIndex = selected.indexOf(id);
    let newSelected = [];

    if (selectedIndex === -1) {
      newSelected = newSelected.concat(selected, id);
    } else if (selectedIndex === 0) {
      newSelected = newSelected.concat(selected.slice(1));
    } else if (selectedIndex === selected.length - 1) {
      newSelected = newSelected.concat(selected.slice(0, -1));
    } else if (selectedIndex > 0) {
      newSelected = newSelected.concat(
        selected.slice(0, selectedIndex),
        selected.slice(selectedIndex + 1),
      );
    }

    this.setState({ selected: newSelected });
  };

  handleChangePage (event,page) {
    this.setState({ page });
  };
  
  handleChangeRowsPerPage (event) {
    this.setState({ page: 0, rowsPerPage: event.target.value });
  };

  isSelected  (id) {return this.state.selected.indexOf(id) !== -1;}

  handleDelete(index) {
    const id= this.state.data.map(row => row.id);
    const data = [...this.state.data];
    index.map( k=> {
      let n = id.indexOf(k);
      id.splice(n,1);
      data.splice(n,1);
    });
    this.setState({data});
    this.setState({selected: []});
  };

  render() {
    const { classes } = this.props;
    const { data, columns, selected, rowsPerPage, page } = this.state;
    const emptyRows = rowsPerPage - Math.min(rowsPerPage, data.length - page * rowsPerPage);
    return (
      <MuiThemeProvider theme={theme} >
        <Paper classes={{root: classes.tableRoot,typography: classes.typography}}>
          <EnhancedTableToolbar numSelected={selected.length} 
                                handleDelete={this.handleDelete.bind(this,selected)}/>
          <div className={classes.tableWrapper}>
            <Table className={classes.table} aria-labelledby="tableTitle">
              <EnhancedTableHead
                numSelected={selected.length}
                onSelectAllClick={this.handleSelectAllClick.bind(this)}
                rowCount={data.length}
                columns={this.state.columns}
              />
              <TableBody>
                {data
                  .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                  .map((data,id) => {
                    const isSelected = this.isSelected(data.id);
                    return (
                      <TableRow
                        hover
                        onChange={this.handleClick.bind(this,(data.id))}
                        role="checkbox"
                        aria-checked={isSelected}
                        tabIndex={-1}
                        selected={isSelected}
                        key={id}
                        id={'container-desk'}
                      >
                        <TableCell padding="checkbox"
                                   className={classes.tableCell}>
                          <Checkbox checked={isSelected} 
                                    color='secondary'
                          />
                        </TableCell>
                          {columns.map((column,id) =>
                          
                          <TableCell className={classes.tableCell} key={id}>{data[column.field]}</TableCell>
                          )}
                          { selected.includes(data.id) ?
                            <TableCell className={classes.tableCell}>
                              <TableMenu handleDelete={this.handleDelete.bind(this,selected)}/>
                            </TableCell> :
                            <TableCell/>
                          }
                        </TableRow>               
                    );                 
                  })}             
                {emptyRows > 0 && (
                  <TableRow style={{ height: 49 * emptyRows }}>
                    <TableCell colSpan={6} className={classes.tableCell}/>
                  </TableRow>
                )}
              </TableBody>
            </Table>
          </div>
          <TablePagination
            labelRowsPerPage={translate('NumberOfRows')}
            rowsPerPageOptions={[5, 10, 25]}
            component="div"
            colSpan={3}
            count={data.length}
            rowsPerPage={rowsPerPage}
            page={page}
            backIconButtonProps={{
              'aria-label': 'Previous Page',
            }}
            nextIconButtonProps={{
              'aria-label': 'Next Page',
            }}
            onChangePage={this.handleChangePage}
            onChangeRowsPerPage={this.handleChangeRowsPerPage}
          />
        </Paper>
      </MuiThemeProvider>
    );
  }
}

export default withStyles(Style)(TableComponent);