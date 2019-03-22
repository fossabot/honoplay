const Style = (theme) => ({
    root: {
      display: 'flex',
      marginTop: theme.spacing.unit * 3,
      overflowX: 'hide',  
    },
    table: {
      minWidth: 100,
    },
    row: {
      '&:nth-of-type(odd)': {
        backgroundColor: theme.palette.background.default,
      },
    },
    tableCell: {
      paddingRight: 4,
      paddingLeft:4,
      borderWidth: 1, 
      borderColor: '#dee2e6',
      borderStyle: 'solid',
      color: '#495057'
    },
    TableHead: {
      paddingRight: 4,
      paddingLeft:4,
      backgroundColor: '#e9ecef',
      color: '#495057',
      fontWeight: 'bold',
      borderWidth: 1, 
      borderColor: '#dee2e6',
      borderStyle: 'solid',
    },
    colorSwitchBase: {
      color: '#52d869',
      '&$colorChecked': {
        color: '#52d869',
        '& + $colorBar': {
          backgroundColor: '#52d869',
        },
      },
    },
    colorBar: {},
    colorChecked: {},
    icon: {
      margin: theme.spacing.unit,
      fontSize: 20,
    },
  });
  

  export default Style;